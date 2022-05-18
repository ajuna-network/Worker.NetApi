﻿namespace Ajuna.NetApi.Model.Types.Base
{
    public class Hash : BasePrim<string>
    {
        public override string TypeName() => "T::Hash";

        public override int TypeSize => 32;

        public override byte[] Encode()
        {
            return Bytes;
        }

        public override void Create(byte[] byteArray)
        {
            Bytes = byteArray;
            Value = Utils.Bytes2HexString(Bytes);
        }
    }
}