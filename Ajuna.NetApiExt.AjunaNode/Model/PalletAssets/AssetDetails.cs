//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Ajuna.NetApi.Model.SpCore;
using Ajuna.NetApi.Model.Types.Base;
using Ajuna.NetApi.Model.Types.Primitive;
using System;
using System.Collections.Generic;


namespace Ajuna.NetApi.Model.PalletAssets
{
    
    
    /// <summary>
    /// >> 121 - Composite[pallet_assets.types.AssetDetails]
    /// </summary>
    public sealed class AssetDetails : BaseType
    {
        
        /// <summary>
        /// >> owner
        /// </summary>
        private Ajuna.NetApi.Model.SpCore.AccountId32 _owner;
        
        /// <summary>
        /// >> issuer
        /// </summary>
        private Ajuna.NetApi.Model.SpCore.AccountId32 _issuer;
        
        /// <summary>
        /// >> admin
        /// </summary>
        private Ajuna.NetApi.Model.SpCore.AccountId32 _admin;
        
        /// <summary>
        /// >> freezer
        /// </summary>
        private Ajuna.NetApi.Model.SpCore.AccountId32 _freezer;
        
        /// <summary>
        /// >> supply
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U128 _supply;
        
        /// <summary>
        /// >> deposit
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U128 _deposit;
        
        /// <summary>
        /// >> min_balance
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U128 _minBalance;
        
        /// <summary>
        /// >> is_sufficient
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.Bool _isSufficient;
        
        /// <summary>
        /// >> accounts
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U32 _accounts;
        
        /// <summary>
        /// >> sufficients
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U32 _sufficients;
        
        /// <summary>
        /// >> approvals
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U32 _approvals;
        
        /// <summary>
        /// >> is_frozen
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.Bool _isFrozen;
        
        public Ajuna.NetApi.Model.SpCore.AccountId32 Owner
        {
            get
            {
                return this._owner;
            }
            set
            {
                this._owner = value;
            }
        }
        
        public Ajuna.NetApi.Model.SpCore.AccountId32 Issuer
        {
            get
            {
                return this._issuer;
            }
            set
            {
                this._issuer = value;
            }
        }
        
        public Ajuna.NetApi.Model.SpCore.AccountId32 Admin
        {
            get
            {
                return this._admin;
            }
            set
            {
                this._admin = value;
            }
        }
        
        public Ajuna.NetApi.Model.SpCore.AccountId32 Freezer
        {
            get
            {
                return this._freezer;
            }
            set
            {
                this._freezer = value;
            }
        }
        
        public Ajuna.NetApi.Model.Types.Primitive.U128 Supply
        {
            get
            {
                return this._supply;
            }
            set
            {
                this._supply = value;
            }
        }
        
        public Ajuna.NetApi.Model.Types.Primitive.U128 Deposit
        {
            get
            {
                return this._deposit;
            }
            set
            {
                this._deposit = value;
            }
        }
        
        public Ajuna.NetApi.Model.Types.Primitive.U128 MinBalance
        {
            get
            {
                return this._minBalance;
            }
            set
            {
                this._minBalance = value;
            }
        }
        
        public Ajuna.NetApi.Model.Types.Primitive.Bool IsSufficient
        {
            get
            {
                return this._isSufficient;
            }
            set
            {
                this._isSufficient = value;
            }
        }
        
        public Ajuna.NetApi.Model.Types.Primitive.U32 Accounts
        {
            get
            {
                return this._accounts;
            }
            set
            {
                this._accounts = value;
            }
        }
        
        public Ajuna.NetApi.Model.Types.Primitive.U32 Sufficients
        {
            get
            {
                return this._sufficients;
            }
            set
            {
                this._sufficients = value;
            }
        }
        
        public Ajuna.NetApi.Model.Types.Primitive.U32 Approvals
        {
            get
            {
                return this._approvals;
            }
            set
            {
                this._approvals = value;
            }
        }
        
        public Ajuna.NetApi.Model.Types.Primitive.Bool IsFrozen
        {
            get
            {
                return this._isFrozen;
            }
            set
            {
                this._isFrozen = value;
            }
        }
        
        public override string TypeName()
        {
            return "AssetDetails";
        }
        
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(Owner.Encode());
            result.AddRange(Issuer.Encode());
            result.AddRange(Admin.Encode());
            result.AddRange(Freezer.Encode());
            result.AddRange(Supply.Encode());
            result.AddRange(Deposit.Encode());
            result.AddRange(MinBalance.Encode());
            result.AddRange(IsSufficient.Encode());
            result.AddRange(Accounts.Encode());
            result.AddRange(Sufficients.Encode());
            result.AddRange(Approvals.Encode());
            result.AddRange(IsFrozen.Encode());
            return result.ToArray();
        }
        
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            Owner = new Ajuna.NetApi.Model.SpCore.AccountId32();
            Owner.Decode(byteArray, ref p);
            Issuer = new Ajuna.NetApi.Model.SpCore.AccountId32();
            Issuer.Decode(byteArray, ref p);
            Admin = new Ajuna.NetApi.Model.SpCore.AccountId32();
            Admin.Decode(byteArray, ref p);
            Freezer = new Ajuna.NetApi.Model.SpCore.AccountId32();
            Freezer.Decode(byteArray, ref p);
            Supply = new Ajuna.NetApi.Model.Types.Primitive.U128();
            Supply.Decode(byteArray, ref p);
            Deposit = new Ajuna.NetApi.Model.Types.Primitive.U128();
            Deposit.Decode(byteArray, ref p);
            MinBalance = new Ajuna.NetApi.Model.Types.Primitive.U128();
            MinBalance.Decode(byteArray, ref p);
            IsSufficient = new Ajuna.NetApi.Model.Types.Primitive.Bool();
            IsSufficient.Decode(byteArray, ref p);
            Accounts = new Ajuna.NetApi.Model.Types.Primitive.U32();
            Accounts.Decode(byteArray, ref p);
            Sufficients = new Ajuna.NetApi.Model.Types.Primitive.U32();
            Sufficients.Decode(byteArray, ref p);
            Approvals = new Ajuna.NetApi.Model.Types.Primitive.U32();
            Approvals.Decode(byteArray, ref p);
            IsFrozen = new Ajuna.NetApi.Model.Types.Primitive.Bool();
            IsFrozen.Decode(byteArray, ref p);
            TypeSize = p - start;
        }
    }
}
