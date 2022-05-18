//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Ajuna.NetApi.Model.Extrinsics;
using Ajuna.NetApi.Model.FrameSupport;
using Ajuna.NetApi.Model.Meta;
using Ajuna.NetApi.Model.PalletBalances;
using Ajuna.NetApi.Model.SpCore;
using Ajuna.NetApi.Model.SpRuntime;
using Ajuna.NetApi.Model.Types;
using Ajuna.NetApi.Model.Types.Base;
using Ajuna.NetApi.Model.Types.Primitive;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Ajuna.NetApi.Model.PalletBalances
{
    
    
    public sealed class BalancesStorage
    {
        
        // Substrate client for the storage calls.
        private SubstrateClientExt _client;
        
        public BalancesStorage(SubstrateClientExt client)
        {
            this._client = client;
            _client.StorageKeyDict.Add(new System.Tuple<string, string>("Balances", "TotalIssuance"), new System.Tuple<Ajuna.NetApi.Model.Meta.Storage.Hasher[], System.Type, System.Type>(null, null, typeof(Ajuna.NetApi.Model.Types.Primitive.U128)));
            _client.StorageKeyDict.Add(new System.Tuple<string, string>("Balances", "Account"), new System.Tuple<Ajuna.NetApi.Model.Meta.Storage.Hasher[], System.Type, System.Type>(new Ajuna.NetApi.Model.Meta.Storage.Hasher[] {
                            Ajuna.NetApi.Model.Meta.Storage.Hasher.BlakeTwo128Concat}, typeof(Ajuna.NetApi.Model.SpCore.AccountId32), typeof(Ajuna.NetApi.Model.PalletBalances.AccountData)));
            _client.StorageKeyDict.Add(new System.Tuple<string, string>("Balances", "Locks"), new System.Tuple<Ajuna.NetApi.Model.Meta.Storage.Hasher[], System.Type, System.Type>(new Ajuna.NetApi.Model.Meta.Storage.Hasher[] {
                            Ajuna.NetApi.Model.Meta.Storage.Hasher.BlakeTwo128Concat}, typeof(Ajuna.NetApi.Model.SpCore.AccountId32), typeof(Ajuna.NetApi.Model.FrameSupport.WeakBoundedVecT3)));
            _client.StorageKeyDict.Add(new System.Tuple<string, string>("Balances", "Reserves"), new System.Tuple<Ajuna.NetApi.Model.Meta.Storage.Hasher[], System.Type, System.Type>(new Ajuna.NetApi.Model.Meta.Storage.Hasher[] {
                            Ajuna.NetApi.Model.Meta.Storage.Hasher.BlakeTwo128Concat}, typeof(Ajuna.NetApi.Model.SpCore.AccountId32), typeof(Ajuna.NetApi.Model.FrameSupport.BoundedVecT1)));
            _client.StorageKeyDict.Add(new System.Tuple<string, string>("Balances", "StorageVersion"), new System.Tuple<Ajuna.NetApi.Model.Meta.Storage.Hasher[], System.Type, System.Type>(null, null, typeof(Ajuna.NetApi.Model.PalletBalances.EnumReleases)));
        }
        
        /// <summary>
        /// >> TotalIssuanceParams
        ///  The total units issued in the system.
        /// </summary>
        public static string TotalIssuanceParams()
        {
            return RequestGenerator.GetStorage("Balances", "TotalIssuance", Ajuna.NetApi.Model.Meta.Storage.Type.Plain);
        }
        
        /// <summary>
        /// >> TotalIssuance
        ///  The total units issued in the system.
        /// </summary>
        public async Task<Ajuna.NetApi.Model.Types.Primitive.U128> TotalIssuance(CancellationToken token)
        {
            string parameters = BalancesStorage.TotalIssuanceParams();
            return await _client.GetStorageAsync<Ajuna.NetApi.Model.Types.Primitive.U128>(parameters, token);
        }
        
        /// <summary>
        /// >> AccountParams
        ///  The Balances pallet example of storing the balance of an account.
        /// 
        ///  # Example
        /// 
        ///  ```nocompile
        ///   impl pallet_balances::Config for Runtime {
        ///     type AccountStore = StorageMapShim<Self::Account<Runtime>, frame_system::Provider<Runtime>, AccountId, Self::AccountData<Balance>>
        ///   }
        ///  ```
        /// 
        ///  You can also store the balance of an account in the `System` pallet.
        /// 
        ///  # Example
        /// 
        ///  ```nocompile
        ///   impl pallet_balances::Config for Runtime {
        ///    type AccountStore = System
        ///   }
        ///  ```
        /// 
        ///  But this comes with tradeoffs, storing account balances in the system pallet stores
        ///  `frame_system` data alongside the account data contrary to storing account balances in the
        ///  `Balances` pallet, which uses a `StorageMap` to store balances data only.
        ///  NOTE: This is only used in the case that this pallet is used to store balances.
        /// </summary>
        public static string AccountParams(Ajuna.NetApi.Model.SpCore.AccountId32 key)
        {
            return RequestGenerator.GetStorage("Balances", "Account", Ajuna.NetApi.Model.Meta.Storage.Type.Map, new Ajuna.NetApi.Model.Meta.Storage.Hasher[] {
                        Ajuna.NetApi.Model.Meta.Storage.Hasher.BlakeTwo128Concat}, new Ajuna.NetApi.Model.Types.IType[] {
                        key});
        }
        
        /// <summary>
        /// >> Account
        ///  The Balances pallet example of storing the balance of an account.
        /// 
        ///  # Example
        /// 
        ///  ```nocompile
        ///   impl pallet_balances::Config for Runtime {
        ///     type AccountStore = StorageMapShim<Self::Account<Runtime>, frame_system::Provider<Runtime>, AccountId, Self::AccountData<Balance>>
        ///   }
        ///  ```
        /// 
        ///  You can also store the balance of an account in the `System` pallet.
        /// 
        ///  # Example
        /// 
        ///  ```nocompile
        ///   impl pallet_balances::Config for Runtime {
        ///    type AccountStore = System
        ///   }
        ///  ```
        /// 
        ///  But this comes with tradeoffs, storing account balances in the system pallet stores
        ///  `frame_system` data alongside the account data contrary to storing account balances in the
        ///  `Balances` pallet, which uses a `StorageMap` to store balances data only.
        ///  NOTE: This is only used in the case that this pallet is used to store balances.
        /// </summary>
        public async Task<Ajuna.NetApi.Model.PalletBalances.AccountData> Account(Ajuna.NetApi.Model.SpCore.AccountId32 key, CancellationToken token)
        {
            string parameters = BalancesStorage.AccountParams(key);
            return await _client.GetStorageAsync<Ajuna.NetApi.Model.PalletBalances.AccountData>(parameters, token);
        }
        
        /// <summary>
        /// >> LocksParams
        ///  Any liquidity locks on some account balances.
        ///  NOTE: Should only be accessed when setting, changing and freeing a lock.
        /// </summary>
        public static string LocksParams(Ajuna.NetApi.Model.SpCore.AccountId32 key)
        {
            return RequestGenerator.GetStorage("Balances", "Locks", Ajuna.NetApi.Model.Meta.Storage.Type.Map, new Ajuna.NetApi.Model.Meta.Storage.Hasher[] {
                        Ajuna.NetApi.Model.Meta.Storage.Hasher.BlakeTwo128Concat}, new Ajuna.NetApi.Model.Types.IType[] {
                        key});
        }
        
        /// <summary>
        /// >> Locks
        ///  Any liquidity locks on some account balances.
        ///  NOTE: Should only be accessed when setting, changing and freeing a lock.
        /// </summary>
        public async Task<Ajuna.NetApi.Model.FrameSupport.WeakBoundedVecT3> Locks(Ajuna.NetApi.Model.SpCore.AccountId32 key, CancellationToken token)
        {
            string parameters = BalancesStorage.LocksParams(key);
            return await _client.GetStorageAsync<Ajuna.NetApi.Model.FrameSupport.WeakBoundedVecT3>(parameters, token);
        }
        
        /// <summary>
        /// >> ReservesParams
        ///  Named reserves on some account balances.
        /// </summary>
        public static string ReservesParams(Ajuna.NetApi.Model.SpCore.AccountId32 key)
        {
            return RequestGenerator.GetStorage("Balances", "Reserves", Ajuna.NetApi.Model.Meta.Storage.Type.Map, new Ajuna.NetApi.Model.Meta.Storage.Hasher[] {
                        Ajuna.NetApi.Model.Meta.Storage.Hasher.BlakeTwo128Concat}, new Ajuna.NetApi.Model.Types.IType[] {
                        key});
        }
        
        /// <summary>
        /// >> Reserves
        ///  Named reserves on some account balances.
        /// </summary>
        public async Task<Ajuna.NetApi.Model.FrameSupport.BoundedVecT1> Reserves(Ajuna.NetApi.Model.SpCore.AccountId32 key, CancellationToken token)
        {
            string parameters = BalancesStorage.ReservesParams(key);
            return await _client.GetStorageAsync<Ajuna.NetApi.Model.FrameSupport.BoundedVecT1>(parameters, token);
        }
        
        /// <summary>
        /// >> StorageVersionParams
        ///  Storage version of the pallet.
        /// 
        ///  This is set to v2.0.0 for new networks.
        /// </summary>
        public static string StorageVersionParams()
        {
            return RequestGenerator.GetStorage("Balances", "StorageVersion", Ajuna.NetApi.Model.Meta.Storage.Type.Plain);
        }
        
        /// <summary>
        /// >> StorageVersion
        ///  Storage version of the pallet.
        /// 
        ///  This is set to v2.0.0 for new networks.
        /// </summary>
        public async Task<Ajuna.NetApi.Model.PalletBalances.EnumReleases> StorageVersion(CancellationToken token)
        {
            string parameters = BalancesStorage.StorageVersionParams();
            return await _client.GetStorageAsync<Ajuna.NetApi.Model.PalletBalances.EnumReleases>(parameters, token);
        }
    }
    
    public sealed class BalancesCalls
    {
        
        /// <summary>
        /// >> transfer
        /// Contains one variant per dispatchable that can be called by an extrinsic.
        /// </summary>
        public static Method Transfer(Ajuna.NetApi.Model.SpRuntime.EnumMultiAddress dest, BaseCom<Ajuna.NetApi.Model.Types.Primitive.U128> value)
        {
            System.Collections.Generic.List<byte> byteArray = new List<byte>();
            byteArray.AddRange(dest.Encode());
            byteArray.AddRange(value.Encode());
            return new Method(5, "Balances", 0, "transfer", byteArray.ToArray());
        }
        
        /// <summary>
        /// >> set_balance
        /// Contains one variant per dispatchable that can be called by an extrinsic.
        /// </summary>
        public static Method SetBalance(Ajuna.NetApi.Model.SpRuntime.EnumMultiAddress who, BaseCom<Ajuna.NetApi.Model.Types.Primitive.U128> new_free, BaseCom<Ajuna.NetApi.Model.Types.Primitive.U128> new_reserved)
        {
            System.Collections.Generic.List<byte> byteArray = new List<byte>();
            byteArray.AddRange(who.Encode());
            byteArray.AddRange(new_free.Encode());
            byteArray.AddRange(new_reserved.Encode());
            return new Method(5, "Balances", 1, "set_balance", byteArray.ToArray());
        }
        
        /// <summary>
        /// >> force_transfer
        /// Contains one variant per dispatchable that can be called by an extrinsic.
        /// </summary>
        public static Method ForceTransfer(Ajuna.NetApi.Model.SpRuntime.EnumMultiAddress source, Ajuna.NetApi.Model.SpRuntime.EnumMultiAddress dest, BaseCom<Ajuna.NetApi.Model.Types.Primitive.U128> value)
        {
            System.Collections.Generic.List<byte> byteArray = new List<byte>();
            byteArray.AddRange(source.Encode());
            byteArray.AddRange(dest.Encode());
            byteArray.AddRange(value.Encode());
            return new Method(5, "Balances", 2, "force_transfer", byteArray.ToArray());
        }
        
        /// <summary>
        /// >> transfer_keep_alive
        /// Contains one variant per dispatchable that can be called by an extrinsic.
        /// </summary>
        public static Method TransferKeepAlive(Ajuna.NetApi.Model.SpRuntime.EnumMultiAddress dest, BaseCom<Ajuna.NetApi.Model.Types.Primitive.U128> value)
        {
            System.Collections.Generic.List<byte> byteArray = new List<byte>();
            byteArray.AddRange(dest.Encode());
            byteArray.AddRange(value.Encode());
            return new Method(5, "Balances", 3, "transfer_keep_alive", byteArray.ToArray());
        }
        
        /// <summary>
        /// >> transfer_all
        /// Contains one variant per dispatchable that can be called by an extrinsic.
        /// </summary>
        public static Method TransferAll(Ajuna.NetApi.Model.SpRuntime.EnumMultiAddress dest, Ajuna.NetApi.Model.Types.Primitive.Bool keep_alive)
        {
            System.Collections.Generic.List<byte> byteArray = new List<byte>();
            byteArray.AddRange(dest.Encode());
            byteArray.AddRange(keep_alive.Encode());
            return new Method(5, "Balances", 4, "transfer_all", byteArray.ToArray());
        }
        
        /// <summary>
        /// >> force_unreserve
        /// Contains one variant per dispatchable that can be called by an extrinsic.
        /// </summary>
        public static Method ForceUnreserve(Ajuna.NetApi.Model.SpRuntime.EnumMultiAddress who, Ajuna.NetApi.Model.Types.Primitive.U128 amount)
        {
            System.Collections.Generic.List<byte> byteArray = new List<byte>();
            byteArray.AddRange(who.Encode());
            byteArray.AddRange(amount.Encode());
            return new Method(5, "Balances", 5, "force_unreserve", byteArray.ToArray());
        }
    }
    
    /// <summary>
    /// >> Endowed
    /// An account was created with some free balance.
    /// </summary>
    public sealed class EventEndowed : BaseTuple<Ajuna.NetApi.Model.SpCore.AccountId32, Ajuna.NetApi.Model.Types.Primitive.U128>
    {
    }
    
    /// <summary>
    /// >> DustLost
    /// An account was removed whose balance was non-zero but below ExistentialDeposit,
    /// resulting in an outright loss.
    /// </summary>
    public sealed class EventDustLost : BaseTuple<Ajuna.NetApi.Model.SpCore.AccountId32, Ajuna.NetApi.Model.Types.Primitive.U128>
    {
    }
    
    /// <summary>
    /// >> Transfer
    /// Transfer succeeded.
    /// </summary>
    public sealed class EventTransfer : BaseTuple<Ajuna.NetApi.Model.SpCore.AccountId32, Ajuna.NetApi.Model.SpCore.AccountId32, Ajuna.NetApi.Model.Types.Primitive.U128>
    {
    }
    
    /// <summary>
    /// >> BalanceSet
    /// A balance was set by root.
    /// </summary>
    public sealed class EventBalanceSet : BaseTuple<Ajuna.NetApi.Model.SpCore.AccountId32, Ajuna.NetApi.Model.Types.Primitive.U128, Ajuna.NetApi.Model.Types.Primitive.U128>
    {
    }
    
    /// <summary>
    /// >> Reserved
    /// Some balance was reserved (moved from free to reserved).
    /// </summary>
    public sealed class EventReserved : BaseTuple<Ajuna.NetApi.Model.SpCore.AccountId32, Ajuna.NetApi.Model.Types.Primitive.U128>
    {
    }
    
    /// <summary>
    /// >> Unreserved
    /// Some balance was unreserved (moved from reserved to free).
    /// </summary>
    public sealed class EventUnreserved : BaseTuple<Ajuna.NetApi.Model.SpCore.AccountId32, Ajuna.NetApi.Model.Types.Primitive.U128>
    {
    }
    
    /// <summary>
    /// >> ReserveRepatriated
    /// Some balance was moved from the reserve of the first account to the second account.
    /// Final argument indicates the destination balance type.
    /// </summary>
    public sealed class EventReserveRepatriated : BaseTuple<Ajuna.NetApi.Model.SpCore.AccountId32, Ajuna.NetApi.Model.SpCore.AccountId32, Ajuna.NetApi.Model.Types.Primitive.U128, Ajuna.NetApi.Model.FrameSupport.EnumBalanceStatus>
    {
    }
    
    /// <summary>
    /// >> Deposit
    /// Some amount was deposited (e.g. for transaction fees).
    /// </summary>
    public sealed class EventDeposit : BaseTuple<Ajuna.NetApi.Model.SpCore.AccountId32, Ajuna.NetApi.Model.Types.Primitive.U128>
    {
    }
    
    /// <summary>
    /// >> Withdraw
    /// Some amount was withdrawn from the account (e.g. for transaction fees).
    /// </summary>
    public sealed class EventWithdraw : BaseTuple<Ajuna.NetApi.Model.SpCore.AccountId32, Ajuna.NetApi.Model.Types.Primitive.U128>
    {
    }
    
    /// <summary>
    /// >> Slashed
    /// Some amount was removed from the account (e.g. for misbehavior).
    /// </summary>
    public sealed class EventSlashed : BaseTuple<Ajuna.NetApi.Model.SpCore.AccountId32, Ajuna.NetApi.Model.Types.Primitive.U128>
    {
    }
    
    public enum BalancesErrors
    {
        
        /// <summary>
        /// >> VestingBalance
        /// Vesting balance too high to send value
        /// </summary>
        VestingBalance,
        
        /// <summary>
        /// >> LiquidityRestrictions
        /// Account liquidity restrictions prevent withdrawal
        /// </summary>
        LiquidityRestrictions,
        
        /// <summary>
        /// >> InsufficientBalance
        /// Balance too low to send value
        /// </summary>
        InsufficientBalance,
        
        /// <summary>
        /// >> ExistentialDeposit
        /// Value too low to create account due to existential deposit
        /// </summary>
        ExistentialDeposit,
        
        /// <summary>
        /// >> KeepAlive
        /// Transfer/payment would kill account
        /// </summary>
        KeepAlive,
        
        /// <summary>
        /// >> ExistingVestingSchedule
        /// A vesting schedule already exists for this account
        /// </summary>
        ExistingVestingSchedule,
        
        /// <summary>
        /// >> DeadAccount
        /// Beneficiary account must pre-exist
        /// </summary>
        DeadAccount,
        
        /// <summary>
        /// >> TooManyReserves
        /// Number of named reserves exceed MaxReserves
        /// </summary>
        TooManyReserves,
    }
}
