using System.Diagnostics;
using Ajuna.NetApi.Model.PalletConnectfour;
using Ajuna.NetApi.Model.Rpc;
using Ajuna.NetApi.Model.SpCore;
using Ajuna.NetApi.Model.Types;
using Ajuna.NetApi.Model.Types.Primitive;
using Ajuna.NetApi.Worker.WebSocketClient;
using NLog;
using NLog.Config;
using NLog.Targets;
using Schnorrkel.Keys;

namespace Ajuna.NetApi.Worker
{
    class Program
    {
        private const string NgrokWebSocketUrl = "ws://b6db-176-92-130-147.ngrok.io/";
        private const string mrenclaveHex = "2qWxMc45Lp5mHuCsnNnD3d63n3cNiC7183FQXDu88ijn";
        
        //private const string Websocketurl = "ws://127.0.0.1:9944";
        
        // Secret Key URI `//Alice` is account:
        // Secret seed:      0xe5be9a5092b81bca64be81d212e7f2f9eba183bb7a90954f7b76361f6edb5c0a
        // Public key(hex):  0xd43593c715fdd31c61141abd04a99fd6822c8558854ccde39a5684e7a56da27d
        // Account ID:       0xd43593c715fdd31c61141abd04a99fd6822c8558854ccde39a5684e7a56da27d
        // SS58 Address:     5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY
        public static MiniSecret MiniSecretAlice => new MiniSecret(Utils.HexToByteArray("0xe5be9a5092b81bca64be81d212e7f2f9eba183bb7a90954f7b76361f6edb5c0a"), ExpandMode.Ed25519);
        public static Account Alice => Account.Build(KeyType.Sr25519, MiniSecretAlice.ExpandToSecret().ToBytes(), MiniSecretAlice.GetPair().Public.Key);

        // Secret Key URI `//Bob` is account:
        // Secret seed:      0x398f0c28f98885e046333d4a41c19cee4c37368a9832c6502f6cfd182e2aef89
        // Public key(hex):  0x8eaf04151687736326c9fea17e25fc5287613693c912909cb226aa4794f26a48
        // Account ID:       0x8eaf04151687736326c9fea17e25fc5287613693c912909cb226aa4794f26a48
        // SS58 Address:     5FHneW46xGXgs5mUiveU4sbTyGBzmstUspZC92UhjJM694ty
        public static MiniSecret MiniSecretBob => new MiniSecret(Utils.HexToByteArray("0x398f0c28f98885e046333d4a41c19cee4c37368a9832c6502f6cfd182e2aef89"), ExpandMode.Ed25519);
        public static Account Bob => Account.Build(KeyType.Sr25519, MiniSecretBob.ExpandToSecret().ToBytes(), MiniSecretBob.GetPair().Public.Key);

        private static async Task Main(string[] args)
        {
            var config = new LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new FileTarget("logfile")
            {
                FileName = "log.txt",
                DeleteOldFileOnStartup = true
            };

            var logconsole = new ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logfile);

            // Apply config           
            LogManager.Configuration = config;

            // Add this to your C# console app's Main method to give yourself
            // a CancellationToken that is canceled when the user hits Ctrl+C.
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                Console.WriteLine("Canceling...");
                cts.Cancel();
                e.Cancel = true;
            };

            try
            {
                Console.WriteLine("Press Ctrl+C to end.");
                await MainAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                // This is the normal way we close.
            }
        }

        private static async Task MainAsync(CancellationToken cancellationToken)
        {
            var ngrok = "wss://9ae9-84-75-48-249.ngrok.io";
            var shardHex = "3JFfg4Ff2SHk7sCsY6nZ59m92vFSCxmWQ1jgh52VzDqT";
            var mrenclaveHex = "3JFfg4Ff2SHk7sCsY6nZ59m92vFSCxmWQ1jgh52VzDqT";

            //await LaunchGameAsync("ws://127.0.0.1:9944");
            //await TestNodeAsync("ws://127.0.0.1:9944");

            //await RunGameAsync("ws://127.0.0.1:2000");

            await RunTransactionTestAsync(
                websocketurl: ngrok,
                shardHex: shardHex,
                mrenclaveHex: mrenclaveHex
            );

            //await RunRPCMethodsTestAsync(
            //    websocketurl: ngrok);
        }        
        
        /// <summary>
        /// Test with the SubstrateTrackingClientExt where the connection is not closed
        /// </summary>
        /// <param name="cancellationToken"></param>
        private static async Task TestWithSubstrateTrackingClientExt(CancellationToken cancellationToken)
        {
            var client = new SubstrateTrackingClientExt(new Uri(NgrokWebSocketUrl));
            
            var shieldingKey = await client.ShieldingKeyAsync();

            var shardHex = mrenclaveHex;
            var player = Alice;

            var balance = await client.GetFreeBalanceAsync(player, shieldingKey, shardHex);
            if (balance != null) Console.WriteLine($"Balance[{player.Value}] = {balance.Value}");

            
            var boardStruct = await client.GetBoardStructAsync(player, shieldingKey, shardHex);
            if (boardStruct != null) PrintBoard(boardStruct);

            Thread.Sleep(1000);

            var hash = await client.PlayTurnAsync(Alice, 3, shieldingKey, shardHex, mrenclaveHex);

            Thread.Sleep(2000);
    
            await client.BalanceTransferAsync(Alice, Bob, (uint)100000, shieldingKey, shardHex, mrenclaveHex);

            Thread.Sleep(2000);

            var balanceOfAlice =  await client.GetFreeBalanceAsync(Alice, shieldingKey, shardHex);
            var balanceOfBob =  await client.GetFreeBalanceAsync(Alice, shieldingKey, shardHex);
            
            Console.WriteLine($"Alice has {balanceOfAlice}");
            Console.WriteLine($"Bob has {balanceOfBob}");

            client.Dispose();
        }

        private static async Task RunGameAsync(string websocketurl)
        {
            /**
             * docker ps
             * docker exec -it 7aeac2a21f93 /bin/bash
             * ./integritee-cli trusted transfer //Alice //Bob 1000 --mrenclave 2CMLqGnL56xp4qkVDq4pmKKYJn4btSGF9brgGEsGW3qm --direct
             */

     
            //docker-c4-worker-1      | MRENCLAVE=2qWxMc45Lp5mHuCsnNnD3d63n3cNiC7183FQXDu88ijn

            var shardHex = "2CMLqGnL56xp4qkVDq4pmKKYJn4btSGF9brgGEsGW3qm";
            var mrenclaveHex = "2qWxMc45Lp5mHuCsnNnD3d63n3cNiC7183FQXDu88ijn";
            
            // It is the same at the time being;
            shardHex = mrenclaveHex;
            var client = new SubstrateClientExt(new Uri(websocketurl));
            
            
            var shieldingKey = await client.ShieldingKeyAsync();

            // - TrustedOperation

            var player = Alice;

             var boardStruct = await client.GetBoardStructAsync(player, shieldingKey, shardHex);
            //if (boardStruct != null) PrintBoard(boardStruct);

            //Thread.Sleep(1000);

            var hash = await client.PlayTurnAsync(Alice, 3, shieldingKey, shardHex, mrenclaveHex);

            //Thread.Sleep(2000);

            //await PrintBoardAsync(Alice, shieldingKey, shardHex);

            Stopwatch sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < 50; i++)
            {
                //code to test
                var balance = await client.GetFreeBalanceAsync(player, shieldingKey, shardHex);
            }
            sw.Stop();
            Console.WriteLine("Time elapsed: {0}", sw.Elapsed);

            //var balance = await client.GetFreeBalanceAsync(player, shieldingKey, shardHex);
            //if (balance != null) Console.WriteLine($"Balance[{player.Value}] = {balance.Value}");

            //Thread.Sleep(2000);

            //await client.BalanceTransferAsync(Alice, Bob, (uint)100000, shieldingKey, shardHex, mrenclaveHex);

            //Thread.Sleep(2000);

            //await client.FreeBalanceAsync(Alice, shieldingKey, shardHex);

            await client.CloseAsync();
        }

 

        /// <summary>
        /// Simple extrinsic tester
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="extrinsicUpdate"></param>
        private static void ActionExtrinsicUpdate(string subscriptionId, ExtrinsicStatus extrinsicUpdate)
        {
            switch (extrinsicUpdate.ExtrinsicState)
            {
                case ExtrinsicState.None:
                    if (extrinsicUpdate.InBlock?.Value.Length > 0) {
                        Console.WriteLine($"{subscriptionId}: InBlock {extrinsicUpdate.InBlock.Value}");
                    } else if (extrinsicUpdate.Finalized?.Value.Length > 0) {
                        Console.WriteLine($"{subscriptionId}: Finalized {extrinsicUpdate.Finalized.Value}");
                    }
                   break;
                case ExtrinsicState.Future:
                    break;
                case ExtrinsicState.Ready:
                    break;
                case ExtrinsicState.Dropped:
                    break;
                case ExtrinsicState.Invalid:
                    break;
                default:
                    break;
            }
        }
        
        
        private static async Task RunTransactionTestAsync(string websocketurl, string shardHex, string mrenclaveHex)
        {
            /**
             * docker ps
             * docker exec -it 7aeac2a21f93 /bin/bash
             * ./integritee-cli trusted transfer //Alice //Bob 1000 --mrenclave 2CMLqGnL56xp4qkVDq4pmKKYJn4btSGF9brgGEsGW3qm --direct
             */

            var client = new SubstrateClientExt(new Uri(websocketurl));

            await client.ConnectAsync(false, false, false, CancellationToken.None);

            var shieldingKey = await client.ShieldingKeyAsync();

            //Thread.Sleep(2000);

            // - TrustedOperation

            var player = Alice;

            //var boardStruct1 = await client.GetBoardStructAsync(player, shieldingKey, shardHex);
            //if (boardStruct1 != null) PrintBoard(boardStruct1);

            //var hash = await client.PlayTurnAsync(Alice, 3, shieldingKey, shardHex, mrenclaveHex);

            Thread.Sleep(2000);

            var balance1 = await client.GetFreeBalanceAsync(player, shieldingKey, shardHex);
            if (balance1 != null) Console.WriteLine($"Balance[{player.Value}] = {balance1.Value}");

            Thread.Sleep(2000);

            await client.BalanceTransferAsync(Alice, Bob, (uint)100000, shieldingKey, shardHex, mrenclaveHex);

            Thread.Sleep(2000);

            var balance2 = await client.GetFreeBalanceAsync(player, shieldingKey, shardHex);
            if (balance2 != null) Console.WriteLine($"Balance[{player.Value}] = {balance2.Value}");

            Thread.Sleep(2000);

            // close connection
            await client.CloseAsync();
        }

        private static async Task TestNodeAsync(string websocketurl)
        {
            var extrinsicWait = 10000;

            var client = new SubstrateClientExt(new Uri(websocketurl));

            var cts = new CancellationTokenSource();
            await client.ConnectAsync(false, true, true, cts.Token);

            var account = new AccountId32();
            account.Create(Utils.GetPublicKeyFrom(Alice.Value));

            var gameQueuePos = await client.SystemStorage.Account(account, CancellationToken.None);

        }

        private static async Task LaunchGameAsync(string websocketurl)
        {
            var extrinsicWait = 10000;

            var client = new SubstrateClientExt(new Uri(websocketurl));

            var cts = new CancellationTokenSource();
            await client.ConnectAsync(false, true, true, cts.Token);

            var gameEngine = new Ajuna.NetApi.Model.PalletGameregistry.GameEngine();
            var id = new U8();
            id.Create(1);
            gameEngine.Id = id;
            var version = new U8();
            version.Create(1);
            gameEngine.Version = version;

            var gameQueuePre = await client.GameRegistryStorage.GameQueues(gameEngine, CancellationToken.None);

            var extrinsicMethod = Ajuna.NetApi.Model.PalletGameRegistry.GameRegistryCalls.Queue();

            // Alice queues for a game ...
            var subscription1 = await client.Author.SubmitAndWatchExtrinsicAsync(ActionExtrinsicUpdate, extrinsicMethod, Alice, 0, 64, cts.Token);
            Console.WriteLine($"Queued Alice {subscription1}");
            Thread.Sleep(extrinsicWait);

            // Bob queues for a game ...
            var subscription2 = await client.Author.SubmitAndWatchExtrinsicAsync(ActionExtrinsicUpdate, extrinsicMethod, Bob, 0, 64, cts.Token);
            Console.WriteLine($"Queued Bob {subscription2}");
            Thread.Sleep(extrinsicWait);

            var gameQueuePos = await client.GameRegistryStorage.GameQueues(gameEngine, CancellationToken.None);

        }

        public static void PrintBoard(BoardStruct boardStruct)
        {
            Console.WriteLine($"Board[{Utils.GetAddressFrom(boardStruct.Id.Value.Bytes)}]: {boardStruct.BoardState.Value}");
            Console.WriteLine($"Red is {Utils.GetAddressFrom(boardStruct.Red.Value.Bytes)}");
            Console.WriteLine($"Blue is {Utils.GetAddressFrom(boardStruct.Blue.Value.Bytes)}");
            Console.WriteLine($"LastTurn is {boardStruct.LastTurn.Value}");
            Console.WriteLine($"NextPlayer is {boardStruct.NextPlayer.Value}");
            var board = new int[7, 6];
            for (int x = 0; x < boardStruct.Board.Value.Length; x++)
            {
                Ajuna.NetApi.Model.Base.Arr6U8 column = boardStruct.Board.Value[x];
                for (int y = 0; y < column.Value.Length; y++)
                {
                    board[x, y] = column.Value[y].Value;
                }
            }

            for (int y = 0; y < board.GetLength(1); y++)
            {
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    Console.Write(board[x, y]);
                }
                Console.WriteLine();
            }

        }
    }

}
