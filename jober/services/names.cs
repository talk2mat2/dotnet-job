using System;
namespace jober.services
{
    public class names:items
    {
        //public names()
        //{ˀ
        //}
        public  string saymyname()
        {
            Guid _id = Guid.NewGuid();
            return Convert.ToString(_id)!;
        }
    }
}

