namespace MaxHotel.Models
{

    using System;

    public class Reserva
    {
        public List<Pessoa> Hospedes { get; set; }
        public Suite Suite { get; set; }
        public int DiasReserva { get; set; }

        public decimal CalcularValorTotal()
        {
            decimal valorBase = Suite.PrecoDiaria * DiasReserva;
            if (DiasReserva > 10)
            {
                return valorBase * 0.90m;
            }
            return valorBase;
        }

    }
}