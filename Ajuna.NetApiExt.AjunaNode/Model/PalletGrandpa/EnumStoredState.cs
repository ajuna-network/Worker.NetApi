//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Ajuna.NetApi.Model.Types.Base;
using Ajuna.NetApi.Model.Types.Primitive;
using System;
using System.Collections.Generic;


namespace Ajuna.NetApi.Model.PalletGrandpa
{
    
    
    public enum StoredState
    {
        
        Live,
        
        PendingPause,
        
        Paused,
        
        PendingResume,
    }
    
    /// <summary>
    /// >> 85 - Variant[pallet_grandpa.StoredState]
    /// </summary>
    public sealed class EnumStoredState : BaseEnumExt<StoredState, BaseVoid, BaseTuple<Ajuna.NetApi.Model.Types.Primitive.U32, Ajuna.NetApi.Model.Types.Primitive.U32>, BaseVoid, BaseTuple<Ajuna.NetApi.Model.Types.Primitive.U32, Ajuna.NetApi.Model.Types.Primitive.U32>>
    {
    }
}
