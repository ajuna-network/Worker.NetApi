//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Ajuna.NetApi.Model.FrameSupport;
using Ajuna.NetApi.Model.Types.Base;
using Ajuna.NetApi.Model.Types.Primitive;
using System;
using System.Collections.Generic;


namespace Ajuna.NetApi.Model.PalletAssets
{
    
    
    /// <summary>
    /// >> 127 - Composite[pallet_assets.types.AssetMetadata]
    /// </summary>
    public sealed class AssetMetadata : BaseType
    {
        
        /// <summary>
        /// >> deposit
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U128 _deposit;
        
        /// <summary>
        /// >> name
        /// </summary>
        private Ajuna.NetApi.Model.FrameSupport.BoundedVecT2 _name;
        
        /// <summary>
        /// >> symbol
        /// </summary>
        private Ajuna.NetApi.Model.FrameSupport.BoundedVecT2 _symbol;
        
        /// <summary>
        /// >> decimals
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U8 _decimals;
        
        /// <summary>
        /// >> is_frozen
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.Bool _isFrozen;
        
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
        
        public Ajuna.NetApi.Model.FrameSupport.BoundedVecT2 Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        
        public Ajuna.NetApi.Model.FrameSupport.BoundedVecT2 Symbol
        {
            get
            {
                return this._symbol;
            }
            set
            {
                this._symbol = value;
            }
        }
        
        public Ajuna.NetApi.Model.Types.Primitive.U8 Decimals
        {
            get
            {
                return this._decimals;
            }
            set
            {
                this._decimals = value;
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
            return "AssetMetadata";
        }
        
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(Deposit.Encode());
            result.AddRange(Name.Encode());
            result.AddRange(Symbol.Encode());
            result.AddRange(Decimals.Encode());
            result.AddRange(IsFrozen.Encode());
            return result.ToArray();
        }
        
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            Deposit = new Ajuna.NetApi.Model.Types.Primitive.U128();
            Deposit.Decode(byteArray, ref p);
            Name = new Ajuna.NetApi.Model.FrameSupport.BoundedVecT2();
            Name.Decode(byteArray, ref p);
            Symbol = new Ajuna.NetApi.Model.FrameSupport.BoundedVecT2();
            Symbol.Decode(byteArray, ref p);
            Decimals = new Ajuna.NetApi.Model.Types.Primitive.U8();
            Decimals.Decode(byteArray, ref p);
            IsFrozen = new Ajuna.NetApi.Model.Types.Primitive.Bool();
            IsFrozen.Decode(byteArray, ref p);
            TypeSize = p - start;
        }
    }
}
