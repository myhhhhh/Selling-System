//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace sale_system_new
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        public string custName { get; set; }
        public string custType { get; set; }
        public string saleWay { get; set; }
        public string custCon { get; set; }
        public string phoneNum { get; set; }
        public string custAddr { get; set; }
        public string machineType { get; set; }
    
        public virtual Machine Machine { get; set; }
    }
}
