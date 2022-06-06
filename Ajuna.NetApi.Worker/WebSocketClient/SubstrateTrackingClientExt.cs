using System.Security.Cryptography;
using Ajuna.NetApi.Model.AjunaWorker;
using Ajuna.NetApi.Model.PrimitiveTypes;
using Ajuna.NetApiExt.Model.AjunaWorker.Helper;
using SimpleBase;

namespace Ajuna.NetApi.Worker.WebSocketClient;


/// <summary>
/// This Substrate Client inherits from SubstrateClientExt and offers a different
/// allows for receiving multiple responses for one request.
/// (Contrary to SubstrateClientExt, this client keeps the connection to the socket open)
/// </summary>
public class SubstrateTrackingClientExt : SubstrateClientExt
{
    private readonly JsonRpcClient _client;

    public SubstrateTrackingClientExt(Uri uri) : base(uri)
    {
        var socket = new WebSocketSharp.WebSocket(uri.ToString());
        socket.Connect();

        _client = new JsonRpcClient(socket);
    }

    public override async Task<RpcReturnValue> ExecuteTrustedOperationAsync(EnumTrustedOperation trustedOperation,
        RSAParameters shieldingKey, string shardHex)
    {
        var cypherText = Wrapper.SignTrustedOperation(shieldingKey, trustedOperation);

        // - ShardIdentifier
        var shardId = new H256();
        shardId.Create(Base58.Bitcoin.Decode(shardHex).ToArray());

        Request initialRequest = new Request
        {
            Shard = shardId,
            CypherText = Wrapper.VecU8FromBytes(cypherText)
        };

        var parameters = initialRequest.Encode().Cast<object>().ToArray();

        var request = _client.CreateRequest("author_submitAndWatchExtrinsic", parameters);

        var result = _client.SendRequest<byte[]>(request);

        var returnValue = new RpcReturnValue();
        returnValue.Create(result);

        return returnValue;
    }
}