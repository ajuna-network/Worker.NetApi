//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Ajuna.NetApi.Model.PrimitiveTypes;
using Ajuna.NetApi.Model.Types.Base;
using Ajuna.NetApi.Model.Types.Primitive;
using System;
using System.Collections.Generic;


namespace Ajuna.NetApi.Model.FinalityGrandpa
{
    
    
    /// <summary>
    /// >> 99 - Composite[finality_grandpa.Precommit]
    /// </summary>
    public sealed class Precommit : BaseType
    {
        
        /// <summary>
        /// >> target_hash
        /// </summary>
        private Ajuna.NetApi.Model.PrimitiveTypes.H256 _targetHash;
        
        /// <summary>
        /// >> target_number
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U32 _targetNumber;
        
        public Ajuna.NetApi.Model.PrimitiveTypes.H256 TargetHash
        {
            get
            {
                return this._targetHash;
            }
            set
            {
                this._targetHash = value;
            }
        }
        
        public Ajuna.NetApi.Model.Types.Primitive.U32 TargetNumber
        {
            get
            {
                return this._targetNumber;
            }
            set
            {
                this._targetNumber = value;
            }
        }
        
        public override string TypeName()
        {
            return "Precommit";
        }
        
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(TargetHash.Encode());
            result.AddRange(TargetNumber.Encode());
            return result.ToArray();
        }
        
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            TargetHash = new Ajuna.NetApi.Model.PrimitiveTypes.H256();
            TargetHash.Decode(byteArray, ref p);
            TargetNumber = new Ajuna.NetApi.Model.Types.Primitive.U32();
            TargetNumber.Decode(byteArray, ref p);
            TypeSize = p - start;
        }
    }
}
