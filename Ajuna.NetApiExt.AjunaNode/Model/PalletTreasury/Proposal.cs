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


namespace Ajuna.NetApi.Model.PalletTreasury
{
    
    
    /// <summary>
    /// >> 165 - Composite[pallet_treasury.Proposal]
    /// </summary>
    public sealed class Proposal : BaseType
    {
        
        /// <summary>
        /// >> proposer
        /// </summary>
        private Ajuna.NetApi.Model.SpCore.AccountId32 _proposer;
        
        /// <summary>
        /// >> value
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U128 _value;
        
        /// <summary>
        /// >> beneficiary
        /// </summary>
        private Ajuna.NetApi.Model.SpCore.AccountId32 _beneficiary;
        
        /// <summary>
        /// >> bond
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U128 _bond;
        
        public Ajuna.NetApi.Model.SpCore.AccountId32 Proposer
        {
            get
            {
                return this._proposer;
            }
            set
            {
                this._proposer = value;
            }
        }
        
        public Ajuna.NetApi.Model.Types.Primitive.U128 Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
        
        public Ajuna.NetApi.Model.SpCore.AccountId32 Beneficiary
        {
            get
            {
                return this._beneficiary;
            }
            set
            {
                this._beneficiary = value;
            }
        }
        
        public Ajuna.NetApi.Model.Types.Primitive.U128 Bond
        {
            get
            {
                return this._bond;
            }
            set
            {
                this._bond = value;
            }
        }
        
        public override string TypeName()
        {
            return "Proposal";
        }
        
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(Proposer.Encode());
            result.AddRange(Value.Encode());
            result.AddRange(Beneficiary.Encode());
            result.AddRange(Bond.Encode());
            return result.ToArray();
        }
        
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            Proposer = new Ajuna.NetApi.Model.SpCore.AccountId32();
            Proposer.Decode(byteArray, ref p);
            Value = new Ajuna.NetApi.Model.Types.Primitive.U128();
            Value.Decode(byteArray, ref p);
            Beneficiary = new Ajuna.NetApi.Model.SpCore.AccountId32();
            Beneficiary.Decode(byteArray, ref p);
            Bond = new Ajuna.NetApi.Model.Types.Primitive.U128();
            Bond.Decode(byteArray, ref p);
            TypeSize = p - start;
        }
    }
}
