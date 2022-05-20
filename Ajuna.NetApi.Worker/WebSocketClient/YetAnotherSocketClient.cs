using System.Security.Cryptography;
using System.Text;
using Ajuna.NetApi.Model.AjunaWorker;
using Ajuna.NetApi.Model.PalletConnectfour;
using Ajuna.NetApi.Model.PrimitiveTypes;
using Ajuna.NetApi.Model.SpCore;
using Ajuna.NetApi.Model.SpRuntime;
using Ajuna.NetApi.Model.Types;
using Ajuna.NetApi.Model.Types.Base;
using Ajuna.NetApi.Model.Types.Primitive;
using Newtonsoft.Json;
using Org.BouncyCastle.Security;
using SimpleBase;

namespace Ajuna.NetApi.Worker.WebSocketClient;

public class YetAnotherSocketClient: IDisposable
{
    private readonly JsonRpcClient _client;
    public YetAnotherSocketClient(String url)
    {
       var  socket = new WebSocketSharp.WebSocket(url);
        socket.Connect();
        
        _client = new JsonRpcClient(socket);
    }
    
    public  RSAParameters  ShieldingKeyAsync()
    {
        var method = "author_getShieldingKey";

        var request = _client.CreateRequest(method, null);

        var response = _client.SendRequest<byte[]>(request);
            
        var rpcReturnValue = new RpcReturnValue();
        rpcReturnValue.Create(response);
        var rsaParameters = new RSAParameters();

        if (rpcReturnValue.DirectRequestStatus.Value == DirectRequestStatus.Ok)
        {
            var rsaPubKeyStr = new UTF8Encoding().GetString(UnwrapBytes(rpcReturnValue.Value.Bytes));
            RSAPubKey rsaPubKey = JsonConvert.DeserializeObject<RSAPubKey>(rsaPubKeyStr);

            rsaParameters.Modulus = rsaPubKey.N.ToArray();
            rsaParameters.Exponent = rsaPubKey.E.ToArray();
            Array.Reverse(rsaParameters.Modulus, 0, rsaParameters.Modulus.Length);
            Array.Reverse(rsaParameters.Exponent, 0, rsaParameters.Exponent.Length);
        }

        return rsaParameters;
    }
    
    public async Task<string> PlayTurnAsync(Account account, byte column, RSAParameters shieldingKey, string shardHex, string mrenclaveHex)
    {
        EnumTrustedOperation tOpNonce = CreateGetter(account, TrustedGetter.Nonce);
        var nonceValue =  ExecuteTrustedOperationAsync(tOpNonce, shieldingKey, shardHex);
        if (Unwrap(SubstrateClientExt.Wrapped.Nonce, nonceValue, out U32 nonce))
        {
            Console.WriteLine($"Account[{account.Value}]({nonce.Value}) play {column}");
            EnumTrustedOperation tOpPlayTurn = CreateCallPlayTurn(account, column, nonce.Value, mrenclaveHex, shardHex);
            var returnValue =  ExecuteTrustedOperationAsync(tOpPlayTurn, shieldingKey, shardHex);
            if (Unwrap(SubstrateClientExt.Wrapped.Hash, returnValue, out H256 value))
            {
                //Console.WriteLine($"Hash is {Utils.Bytes2HexString(value.Value.Bytes)}");
                return Utils.Bytes2HexString(value.Value.Bytes);
            }
        }

        return null;
    }

    public async Task<BoardStruct> GetBoardStructAsync(Account account, RSAParameters shieldingKey, string shardHex)
    {
        EnumTrustedOperation tOpBoard = CreateGetter(account, TrustedGetter.Board);
        var boardValue =  ExecuteTrustedOperationAsync(tOpBoard, shieldingKey, shardHex);
        if (Unwrap(SubstrateClientExt.Wrapped.Board, boardValue, out BoardStruct boardStruct))
        {
            return boardStruct;
        }

        return null;
    }
    
    public async Task<Balance> GetFreeBalanceAsync(Account account, RSAParameters shieldingKey, string shardHex)
    {
        EnumTrustedOperation tOpPreBalance = CreateGetter(account, TrustedGetter.FreeBalance);
        var balanceValuePre =  ExecuteTrustedOperationAsync(tOpPreBalance, shieldingKey, shardHex);
        if (Unwrap(SubstrateClientExt.Wrapped.Balance, balanceValuePre, out Balance balance))
        {
            return balance;
        }
        return null;
    }
    public async Task<string> BalanceTransferAsync(Account fromAccount, Account toAccount, uint amount, RSAParameters shieldingKey, string shardHex, string mrenclaveHex)
    {
        EnumTrustedOperation tOpNonce = CreateGetter(fromAccount, TrustedGetter.Nonce);
        var nonceValue =  ExecuteTrustedOperationAsync(tOpNonce, shieldingKey, shardHex);
        if (Unwrap(SubstrateClientExt.Wrapped.Nonce, nonceValue, out U32 nonce))
        {
            Console.WriteLine($"Account[{fromAccount.Value}]({nonce.Value}) transfers {amount} to Account[{toAccount.Value}]");
            EnumTrustedOperation tOpTransfer = CreateCallBalanceTransfer(fromAccount, toAccount, amount, nonce.Value, mrenclaveHex, shardHex);
            var returnValue =  ExecuteTrustedOperationAsync(tOpTransfer, shieldingKey, shardHex);
            if (Unwrap(SubstrateClientExt.Wrapped.Hash, returnValue, out H256 value))
            {
                //Console.WriteLine($"Hash is {Utils.Bytes2HexString(value.Value.Bytes)}");
                return Utils.Bytes2HexString(value.Value.Bytes);
            }
        }

        return null;
    }
    
    public EnumTrustedOperation CreateCallBalanceTransfer(Account fromAccount, Account toAccount, uint amount, uint nonce, string mrenclaveHex, string shardHex)
    {
        var aliceAccount = new AccountId32();
        aliceAccount.Create(fromAccount.Bytes);

        var bobAccount = new AccountId32();
        bobAccount.Create(toAccount.Bytes);

        var balance = new Balance();
        balance.Create(new System.Numerics.BigInteger(amount));

        var balanceTransferTuple = new BaseTuple<AccountId32, AccountId32, Balance>();
        balanceTransferTuple.Create(aliceAccount, bobAccount, balance);

        var trustedCall = new EnumTrustedCall();
        trustedCall.Create(TrustedCall.BalanceTransfer, balanceTransferTuple);

        var index = new Ajuna.NetApi.Model.AjunaWorker.Index();
        index.Create(nonce);

        var mrenclave = new H256();
        mrenclave.Create(Base58.Bitcoin.Decode(mrenclaveHex).ToArray());

        var shard = new ShardIdentifier();
        shard.Create(Base58.Bitcoin.Decode(shardHex).ToArray());

        var trustedCallPayload = new TrustedCallPayload
        {
            Call = trustedCall,
            Nonce = index,
            Mrenclave = mrenclave,
            Shard = shard
        };

        return GetEnumTrustedOperation(fromAccount, trustedCallPayload);
    }
    
    
    public RpcReturnValue ExecuteTrustedOperationAsync(EnumTrustedOperation trustedOperation, RSAParameters shieldingKey, string shardHex)
    {
        var cypherText = SignTrustedOperation(shieldingKey, trustedOperation);

        // - ShardIdentifier
        var shardId = new H256();
        shardId.Create(Base58.Bitcoin.Decode(shardHex).ToArray());

        Request initialRequest = new Request
        {
            Shard = shardId,
            CypherText = VecU8FromBytes(cypherText)
        };

        // open connection
        //await ConnectAsync(false, false, false, CancellationToken.None);

        var parameters = initialRequest.Encode().Cast<object>().ToArray();

        var request = _client.CreateRequest("author_submitAndWatchExtrinsic", parameters);

        var result = _client.SendRequest<byte[]>(request);

       // var result = await InvokeAsync<byte[]>("author_submitAndWatchExtrinsic", parameters, CancellationToken.None);

        var returnValue = new RpcReturnValue();
        returnValue.Create(result);

        return returnValue;

    }
    
    
 
        public bool Unwrap<T>(SubstrateClientExt.Wrapped wrapped, RpcReturnValue returnValue, out T result) where T : IType, new()
        {
            result = new T();

            switch (returnValue.DirectRequestStatus.Value)
            {
                case DirectRequestStatus.Ok:
                    break;

                case DirectRequestStatus.TrustedOperationStatus:

                    var valueBytes = returnValue.Value.Value.Select(p => p.Value).ToArray();

                    switch (wrapped)
                    {
                        case SubstrateClientExt.Wrapped.Nonce:
                            var nonceWrapped = new BaseOpt<BaseVec<U8>>();
                            nonceWrapped.Create(valueBytes);
                            if (nonceWrapped.OptionFlag)
                            {
                                var bytes = nonceWrapped.Value.Value.Select(p => p.Value).ToArray();
                                result.Create(bytes);
                                return true;
                            }
                            break;

                        case SubstrateClientExt.Wrapped.Balance:
                            var balanceWrapped = new BaseOpt<BaseVec<U8>>();
                            balanceWrapped.Create(valueBytes);
                            if (balanceWrapped.OptionFlag)
                            {
                                var bytes = balanceWrapped.Value.Value.Select(p => p.Value).ToArray();
                                result.Create(bytes);
                                return true;
                            }
                            break;

                        case SubstrateClientExt.Wrapped.Hash:
                            result.Create(valueBytes);
                            return true;

                        case SubstrateClientExt.Wrapped.Board:
                            var boardWrapped = new BaseOpt<BaseVec<U8>>();
                            boardWrapped.Create(valueBytes);
                            if (boardWrapped.OptionFlag)
                            {
                                var bytes = boardWrapped.Value.Value.Select(p => p.Value).ToArray();
                                result.Create(bytes);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                    }


                    break;

                case DirectRequestStatus.Error:
                    var byteArray = returnValue.Value.Bytes;
                    PrintBytes(UnwrapBytes(byteArray));
                    break;
            }

            return false;
        }
    public EnumTrustedOperation CreateGetter(Account accountName, TrustedGetter trustedGetter)
    {
        var account = new AccountId32();
        account.Create(accountName.Bytes);

        var enumTrustedGetter = new EnumTrustedGetter();
        enumTrustedGetter.Create(trustedGetter, account);

        return GetEnumTrustedOperation(accountName, enumTrustedGetter);
    }

    
    public EnumTrustedOperation GetEnumTrustedOperation(Account account, EnumTrustedGetter trustedGetter)
    {
        var signature = new Signature64();
        var signatureArray = Schnorrkel.Sr25519v091.SignSimple(Utils.GetPublicKeyFrom(account.Value), account.PrivateKey, trustedGetter.Encode());
        signature.Create(signatureArray);

        var enumMultiSignature = new EnumMultiSignature();
        enumMultiSignature.Create(MultiSignature.Sr25519, signature);

        var trustedGetterSigned = new TrustedGetterSigned();
        trustedGetterSigned.Getter = trustedGetter;
        trustedGetterSigned.Signature = enumMultiSignature;

        var getter = new EnumGetter();
        getter.Create(Getter.Trusted, trustedGetterSigned);

        var trustedOperation = new EnumTrustedOperation();
        trustedOperation.Create(TrustedOperation.Get, getter);

        return trustedOperation;
    }
    
    public EnumTrustedOperation CreateCallPlayTurn(Account account, byte move, uint nonce, string mrenclaveHex, string shardHex)
    {
        var accountId32 = new AccountId32();
        accountId32.Create(account.Bytes);

        var column = new U8();
        column.Create(move);

        var playTurnTuple = new BaseTuple<AccountId32, U8>();
        playTurnTuple.Create(accountId32, column);

        var trustedCall = new EnumTrustedCall();
        trustedCall.Create(TrustedCall.ConnectfourPlayTurn, playTurnTuple);

        var index = new Ajuna.NetApi.Model.AjunaWorker.Index();
        index.Create(nonce);

        var mrenclave = new H256();
        mrenclave.Create(Base58.Bitcoin.Decode(mrenclaveHex).ToArray());

        var shard = new ShardIdentifier();
        shard.Create(Base58.Bitcoin.Decode(shardHex).ToArray());

        var trustedCallPayload = new TrustedCallPayload
        {
            Call = trustedCall,
            Nonce = index,
            Mrenclave = mrenclave,
            Shard = shard
        };

        return GetEnumTrustedOperation(account, trustedCallPayload);
    }
    
    
    public EnumTrustedOperation GetEnumTrustedOperation(Account account, TrustedCallPayload trustedCallPayload)
    {
        var signature = new Signature64();
        var signatureArray = Schnorrkel.Sr25519v091.SignSimple(Utils.GetPublicKeyFrom(account.Value), account.PrivateKey, trustedCallPayload.Encode());
        signature.Create(signatureArray);

        var enumMultiSignature = new EnumMultiSignature();
        enumMultiSignature.Create(MultiSignature.Sr25519, signature);

        var trustedCallSigned = new TrustedCallSigned();
        trustedCallSigned.Call = trustedCallPayload.Call;
        trustedCallSigned.Nonce = trustedCallPayload.Nonce;
        trustedCallSigned.Signature = enumMultiSignature;

        var trustedOperation = new EnumTrustedOperation();
        trustedOperation.Create(TrustedOperation.DirectCall, trustedCallSigned);

        return trustedOperation;
    }
    
    public byte[] SignTrustedOperation(RSAParameters shieldingKey, EnumTrustedOperation trustedOperation)
    {
        // - Encrypt Encoded TrustedOperation with RSAPubKey
        var keyPair = DotNetUtilities.GetRsaPublicKey(shieldingKey);
        return Utils.RSAEncryptBouncy(trustedOperation.Encode(), keyPair);
    }

    public byte[] UnwrapBytes(byte[] byteArray)
    {
        var baseVec1 = new BaseVec<U8>();
        baseVec1.Create(byteArray);

        var bytes1 = new List<byte>();
        foreach (var by in baseVec1.Value)
        {
            bytes1.Add(by.Value);
        }
        var baseVec2 = new BaseVec<U8>();
        baseVec2.Create(bytes1.ToArray());

        var bytes2 = new List<byte>();
        foreach (var by in baseVec2.Value)
        {
            bytes2.Add(by.Value);
        }
        return bytes2.ToArray();
    }

    public void PrintBytes(byte[] bytes)
    {
        var converter = new UTF8Encoding();
        var str = converter.GetString(bytes);
        Console.WriteLine(str);
    }

    public BaseVec<U8> VecU8FromBytes(byte[] vs)
    {
        var u8list = new List<U8>();
        for (int i = 0; i < vs.Length; i++)
        {
            var u8 = new U8();
            u8.Create(vs[i]);
            u8list.Add(u8);
        }
        var u8Array = u8list.ToArray();

        var result = new BaseVec<U8>();
        result.Create(u8Array);

        return result;
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}