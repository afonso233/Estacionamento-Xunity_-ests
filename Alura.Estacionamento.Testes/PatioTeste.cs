﻿using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class PatioTeste : IDisposable
    {
        private Veiculo veiculo;
        private Patio estacionamento;
        private Operador operador;
        public ITestOutputHelper SaidaConsoleTeste;
        public PatioTeste(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor invocado.");
            veiculo = new Veiculo();
            estacionamento = new Patio();
            operador = new Operador();
            operador.Nome = "Pedro Fagundes";
        }

        [Fact]
        public void ValidaFaturamentoDoEstacionamentoComUmVeiculo()
        {
            //Arrange
            //var estacionamento = new Patio();
            //Operador operador = new Operador;
           //operador.Nome = "Pedro Fagundes";
            
           estacionamento.OperadorPatio = operador;
            //var veiculo = new Veiculo();
            veiculo.Proprietario = "André Silva";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "asd-9999";

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);

        }

        [Theory]
        [InlineData("André Silva", "ASD-1498", "Preto", "Gol")]
        [InlineData("José Silva", "POL-9242", "Cinza", "Fusca")]
        [InlineData("Maria Silva", "GDR-6524", "Azul", "Opala")]
        [InlineData("Pedro Silva", "GDR-0101", "Azul", "Corsa")]
        public void ValidaFaturamentoDoEstacionamentoComVariosVeiculos(string propietario,
            string placa, string cor, string modelo)
        {
            //Arrange
            //var estacionamento = new Patio();
            estacionamento.OperadorPatio = operador;
            //var veiculo = new Veiculo();
            veiculo.Proprietario = propietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("André Silva", "ASD-1498", "Preto", "Gol")]
        public void LocalizaVeiculoNoPatioComBaseNoIdTicket(string propietario,
            string placa, string cor, string modelo)
        {
            //Arrange
            //var estacionamento = new Patio();
            estacionamento.OperadorPatio = operador;
            //var veiculo = new Veiculo();
            veiculo.Proprietario = propietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = estacionamento.PesquisaVeiculo(veiculo.IdTicket);

            //Assert
            Assert.Contains("### Ticket Estacionamento Alura ###", consultado.Ticket);
        }

        [Fact]
        public void AlterarDadosDoPropioVeiculo()
        {
            //Arrange
            //Patio estacionamento = new Patio();
            estacionamento.OperadorPatio = operador;
            //var veiculo = new Veiculo();
            veiculo.Proprietario = "José Silva";
            veiculo.Placa = "EXC-8524";
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Opala";
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            var veiculoAlterado = new Veiculo();
            veiculoAlterado.Proprietario = "José Silva";
            veiculoAlterado.Placa = "EXC-8524";
            veiculoAlterado.Cor = "Preto"; //Alterado
            veiculoAlterado.Modelo = "Opala";

            //Act
            Veiculo alterado = estacionamento.AlterarDadosVeiculo(veiculoAlterado);

            //Assert
            Assert.Equal(alterado.Cor, veiculoAlterado.Cor);

        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Dispose invocado.");
        }
    }
}
