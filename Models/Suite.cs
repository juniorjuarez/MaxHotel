namespace MaxHotel.Models{

using System;

    public class Suite
    {
        public int Numero { get; set; }
        public string Tipo { get; set; }
        public decimal PrecoDiaria { get; set; }
        public bool Disponivel { get; set; }
        public int Capacidade { get; set; }
        public Suite(int numero, string tipo, bool disponivel, decimal precoDiaria, int capacidade  )
        {
            Numero = numero;
            Tipo = tipo;
            PrecoDiaria = precoDiaria;
            Disponivel = disponivel;
            Capacidade = capacidade;
            
        }

    }
}