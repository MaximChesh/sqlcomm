//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public int ID_STUDENT { get; set; }
        public int NOM_ZACHETKI { get; set; }
        public string FAMILIYA { get; set; }
        public string IMYA { get; set; }
        public string OTCHESTVO { get; set; }
        public string POL { get; set; }
        public int ID_SPECIALNOST { get; set; }
        public int ID_FAKULTET { get; set; }
        public Nullable<System.DateTime> DATA_POSTUPLENIA { get; set; }
    
        public virtual FACULTET FACULTET { get; set; }
        public virtual KOLICHESTVO_CHASOV KOLICHESTVO_CHASOV { get; set; }
        public virtual KUR KUR { get; set; }
        public virtual OCENKA OCENKA { get; set; }
        public virtual SPECIALNOST SPECIALNOST { get; set; }
    }
}
