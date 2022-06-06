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


namespace Ajuna.NetApi.Model.FrameSystem
{
    
    
    /// <summary>
    /// >> 76 - Composite[frame_system.limits.BlockWeights]
    /// </summary>
    public sealed class BlockWeights : BaseType
    {
        
        /// <summary>
        /// >> base_block
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U64 _baseBlock;
        
        /// <summary>
        /// >> max_block
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U64 _maxBlock;
        
        /// <summary>
        /// >> per_class
        /// </summary>
        private Ajuna.NetApi.Model.FrameSupport.PerDispatchClassT2 _perClass;
        
        public Ajuna.NetApi.Model.Types.Primitive.U64 BaseBlock
        {
            get
            {
                return this._baseBlock;
            }
            set
            {
                this._baseBlock = value;
            }
        }
        
        public Ajuna.NetApi.Model.Types.Primitive.U64 MaxBlock
        {
            get
            {
                return this._maxBlock;
            }
            set
            {
                this._maxBlock = value;
            }
        }
        
        public Ajuna.NetApi.Model.FrameSupport.PerDispatchClassT2 PerClass
        {
            get
            {
                return this._perClass;
            }
            set
            {
                this._perClass = value;
            }
        }
        
        public override string TypeName()
        {
            return "BlockWeights";
        }
        
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(BaseBlock.Encode());
            result.AddRange(MaxBlock.Encode());
            result.AddRange(PerClass.Encode());
            return result.ToArray();
        }
        
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            BaseBlock = new Ajuna.NetApi.Model.Types.Primitive.U64();
            BaseBlock.Decode(byteArray, ref p);
            MaxBlock = new Ajuna.NetApi.Model.Types.Primitive.U64();
            MaxBlock.Decode(byteArray, ref p);
            PerClass = new Ajuna.NetApi.Model.FrameSupport.PerDispatchClassT2();
            PerClass.Decode(byteArray, ref p);
            TypeSize = p - start;
        }
    }
}
