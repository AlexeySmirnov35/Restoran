//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Restoran
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservations
    {
        public int ReservationsID { get; set; }
        public string FIO { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> NumberOfPeople { get; set; }
        public Nullable<System.DateTime> DateTimeReserv { get; set; }
        public Nullable<int> RoomID { get; set; }
        public Nullable<int> Hours { get; set; }
        public Nullable<System.DateTime> DateEndReserv { get; set; }
        public Nullable<int> DopTimeMinut { get; set; }
    
        public virtual Rooms Rooms { get; set; }
        public virtual User User { get; set; }
    }
}
