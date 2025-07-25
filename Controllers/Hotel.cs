namespace MaxHotel.Controllers
{
    using System;
    using System.IO;                // <-- ADICIONE PARA MANIPULAR ARQUIVOS
    using System.Text.Json;
    using MaxHotel.Models;


    public class Hotel
    {

        private const string DbHotel = "DbHotel.json";

        private List<Suite> suitesDisponiveis = new List<Suite>();
        private List<Reserva> reservasFeitas = new List<Reserva>();




        public void LitaSuitesDisponiveis()
        {
            Console.Clear();
            Console.WriteLine("Lista de suítes disponíveis:");
            Console.WriteLine("\n============================================");



            foreach (var suite in suitesDisponiveis)
            {
                if (!suite.Disponivel)
                {
                    continue;
                }
                Console.WriteLine($"Número: {suite.Numero}, Tipo: {suite.Tipo}, Preço: {suite.PrecoDiaria:C}, Disponível: {suite.Disponivel}, Capacidade: {suite.Capacidade}");
            }
        }


        public void RealizaCheckIn()
        {
            LitaSuitesDisponiveis();

            Console.WriteLine("\n============================================");
            Console.Write("Digite o número da suíte para Check In: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int numeroSuite))
            {
                Console.WriteLine($"\nOk! Processando reserva para a suíte {numeroSuite}...");

                Suite suiteSelecionada = suitesDisponiveis.FirstOrDefault(s => s.Numero == numeroSuite);

                if (suiteSelecionada != null && suiteSelecionada.Disponivel)
                {
                    Console.WriteLine($"\nÓtima escolha! Você selecionou a suíte '{suiteSelecionada.Tipo}' - {suiteSelecionada.Numero}.");

                    Console.Write("\nPor quantos dias deseja reservar esta suíte? ");
                    string inputDias = Console.ReadLine();
                    if (int.TryParse(inputDias, out int diasReserva))
                    {
                        Console.WriteLine($"Ok, a reserva será por {diasReserva} dias.");

                        Console.Write("\nQuantos hóspedes ficarão na suíte? ");
                        if (int.TryParse(Console.ReadLine(), out int quantidadeHospedes) && quantidadeHospedes > 0)
                        {
                            if (quantidadeHospedes <= suiteSelecionada.Capacidade)
                            {
                                List<Pessoa> hospedes = new List<Pessoa>();
                                for (int i = 0; i < quantidadeHospedes; i++)
                                {
                                    Console.Write("Digite o nome completo: ");
                                    string nomeHospede = Console.ReadLine();
                                    hospedes.Add(new Pessoa(nomeHospede));
                                }

                                Console.WriteLine("Hospedes cadastrados com sucesso!");
                                Reserva novaReserva = new Reserva
                                {
                                    Suite = suiteSelecionada,
                                    Hospedes = hospedes,
                                    DiasReserva = diasReserva
                                };

                                reservasFeitas.Add(novaReserva);
                                suiteSelecionada.Disponivel = false;
                                SalvarDados();

                                decimal valorFinal = novaReserva.CalcularValorTotal();

                                Console.WriteLine("\n============================================");
                                Console.WriteLine("      RESERVA CONFIRMADA COM SUCESSO!      ");
                                Console.WriteLine("============================================");
                                Console.WriteLine($"Suíte: {novaReserva.Suite.Tipo} - N° {novaReserva.Suite.Numero}");
                                Console.WriteLine($"Hóspedes: {novaReserva.Hospedes.Count}");
                                Console.WriteLine($"Período: {novaReserva.DiasReserva} dias");
                                Console.WriteLine($"VALOR FINAL: {valorFinal:C} {(diasReserva > 10 ? "(10% de desconto aplicado)" : "")}");
                                Console.WriteLine("============================================");

                            }
                            else
                            {
                                Console.WriteLine($"Erro: A quantidade de hóspedes ({quantidadeHospedes}) excede a capacidade da suíte ({suiteSelecionada.Capacidade}).");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Quantidade de Hospedes invalida");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Número de dias inválido.");
                    }
                }
                else
                {

                    Console.WriteLine("\nErro: Suíte não encontrada ou já está ocupada.");
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida. Por favor, digite apenas o número da suíte.");
            }
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();




        }
        public void RealizaCheckOut()
        {
            Console.WriteLine("Informe o número da súite");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int numeroSuite))
            {
                Reserva reservaCheckout = reservasFeitas.FirstOrDefault(r => r.Suite.Numero == numeroSuite);
                if (reservaCheckout != null)
                {

                    Console.WriteLine($"Encontrada reserva para a suíte {reservaCheckout.Suite.Tipo}, (Nº {reservaCheckout.Suite.Numero})");

                    foreach (var hospede in reservaCheckout.Hospedes)
                    {
                        Console.WriteLine($"Hospede:{hospede.Nome}");
                    }
                    Console.WriteLine($"Hóspedes: {reservaCheckout.Hospedes.Count}");
                    Console.WriteLine($"Período: {reservaCheckout.DiasReserva} dias");
                    Console.WriteLine($"Valor Pago: {reservaCheckout.CalcularValorTotal():C}");

                    reservaCheckout.Suite.Disponivel = true;
                    reservasFeitas.Remove(reservaCheckout);
                    SalvarDados();

                    Console.WriteLine("Checkout realizado, volte sempre.");
                }
                else
                {
                    Console.WriteLine("Nenhuma reserva ativa encontrada para esta suíte.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Por favor, digite um número.");
            }


            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }
        public void ListaReservas()
        {
            Console.Clear();
            Console.WriteLine("--- Todas as Reservas Cadastradas ---");
            if (reservasFeitas.Any())
            {
                foreach (var reserva in reservasFeitas)
                {

                    Console.WriteLine($"Suíte: {reserva.Suite.Tipo} (Nº {reserva.Suite.Numero})");
                    foreach (var hospede in reserva.Hospedes)
                    {
                        Console.WriteLine($"Hospede:{hospede.Nome}");
                    }
                    Console.WriteLine($"Hóspedes: {reserva.Hospedes.Count}");
                    Console.WriteLine($"Período: {reserva.DiasReserva} dias");
                    Console.WriteLine($"Valor Pago: {reserva.CalcularValorTotal():C}");

                }
            }
            else
            {

                Console.WriteLine("\nNenhuma reserva foi feita ainda.");
            }
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }

        public void SalvarDados()
        {
            var dadosParaSalvar = new DadosHotel
            {
                Suites = suitesDisponiveis,
                Reservas = reservasFeitas
            };
            string jsonString = JsonSerializer.Serialize(dadosParaSalvar, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DbHotel, jsonString);
        }

        public void CarregaDados()
        {
            if (File.Exists(DbHotel))
            {

                string jsonString = File.ReadAllText(DbHotel);

                if (string.IsNullOrEmpty(jsonString)) return;



                DadosHotel dadosCarregados = JsonSerializer.Deserialize<DadosHotel>(jsonString);

                if (CarregaDados != null)
                {
                    suitesDisponiveis = dadosCarregados.Suites;
                    reservasFeitas = dadosCarregados.Reservas;

                    foreach (var reserva in reservasFeitas)
                    {
                        Suite suiteOriginal = suitesDisponiveis.FirstOrDefault(s => s.Numero == reserva.Suite.Numero);
                        if (suiteOriginal != null)
                        {
                            reserva.Suite = suiteOriginal;
                        }
                    }
                }

            }
            else
            {
                File.Create(DbHotel).Close();
            }

        }

        public Hotel()
        {
            CarregaDados();
            if (!suitesDisponiveis.Any())
            {
                suitesDisponiveis.Add(new Suite(101, "Luxo", true, 500.00m, 2));
                suitesDisponiveis.Add(new Suite(201, "Standard", true, 300.00m, 3));
                suitesDisponiveis.Add(new Suite(301, "Econômica", true, 200.00m, 4));

            }

        }
    }
}