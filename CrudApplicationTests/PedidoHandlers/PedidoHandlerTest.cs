using ANSYS.Application.Global.PedidoItens.Commands;
using ANSYS.Application.Global.Pedidos.Commands;
using ANSYS.Application.Global.Pedidos.Handlers;
using ANSYS.Application.Global.Usuarios.Commands;
using ANSYS.Domain.Abstractions.Context.EntityFramework;
using ANSYS.Domain.Abstractions.Mappers;
using ANSYS.Domain.Global.Pedidos.Entities;
using ANSYS.Domain.Global.Pedidos.Enums;
using ANSYS.Domain.Global.Pedidos.Repositories;
using ANSYS.Domain.Global.Usuarios.Entities;
using CrudApplicationTests.UsuarioHandlers;
using MediatR;
using Moq;

namespace CrudApplicationTests.PedidoHandlers
{
    public class PedidoHandlerTest
    {
        private readonly Mock<IEntityFrameworkDBContext> _dbcotextMock;
        private readonly Mock<IPedidoRepository> _repositoryMock;
        private readonly Mock<IEntityMapper<Pedido, PedidoCommandInsert, PedidoCommandUpdate>> _mapperMock;
        private readonly Mock<ISender> _senderMock;
        private readonly PedidoHandler pedidoHandler;

        public PedidoHandlerTest()
        {
            _dbcotextMock = new Mock<IEntityFrameworkDBContext>();
            _repositoryMock = new Mock<IPedidoRepository>();
            _mapperMock = new Mock<IEntityMapper<Pedido, PedidoCommandInsert, PedidoCommandUpdate>>();
            _senderMock = new Mock<ISender>();

            pedidoHandler = new PedidoHandler(
                _dbcotextMock.Object,
                _repositoryMock.Object,
                _mapperMock.Object,
                _senderMock.Object);
        }

        [Fact]
        public async Task ListarPedidosAsync_DeveRetornarListaDePedidos_SemFiltro()
        {
            //Arrange
            var listPedidoFake = AuxPedidoFaker.ObterFakesToMock();
            var listUsuarioFake = AuxUsuarioFaker.ObterUsuariosFakeToMock();
            var valorEsperado = listPedidoFake.Count();

            //Act
            _repositoryMock.Setup(r => r.GetAll(default)).ReturnsAsync(listPedidoFake);
            _senderMock.Setup(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default)).ReturnsAsync(listUsuarioFake);

            var resultado = await pedidoHandler.Handle(new PedidoCommandGetAll(string.Empty, null, EStatusPedido.Todos), default);
            var valorRetornado = resultado.Count();

            //Assert
            Assert.NotNull(resultado);
            Assert.Equal(valorRetornado, valorEsperado);
            _repositoryMock.Verify(r => r.GetAll(default), Times.Once);
            _senderMock.Verify(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default), Times.Never);
        }

        [Fact]
        public async Task ListarPedidosAsync_DeveRetornarListaPedidos_ComFiltroCliente()
        {
            //Arrange
            var listPedidoFake = AuxPedidoFaker.ObterFakesToMock();
            var usuarioFake = AuxUsuarioFaker.ObterUsuarioFakeToMock(1);
            var usuariosFake = AuxUsuarioFaker.ObterUsuariosFakeToMock();

            var valorEsperado = listPedidoFake.Where(l => l.ClienteId == usuarioFake.Id).Count();

            //Act
            _repositoryMock.Setup(r => r.GetAll(default)).ReturnsAsync(listPedidoFake);
            _senderMock.Setup(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default)).ReturnsAsync(usuariosFake);

            var resultado = await pedidoHandler.Handle(new PedidoCommandGetAll(usuarioFake.Nome, null, EStatusPedido.Todos), default);
            var valorRetornado = resultado.Count();

            //Assert
            Assert.NotNull(resultado);
            Assert.Equal(valorRetornado, valorEsperado);
            _repositoryMock.Verify(r => r.GetAll(default), Times.Once);
            _senderMock.Verify(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default), Times.Once);
        }

        [Fact]
        public async Task ListarPedidosAsync_DeveRetornarListaPedidos_ComFiltroPedidoId()
        {
            //Arrange
            var listPedidoFake = AuxPedidoFaker.ObterFakesToMock();
            var pedidoFake = AuxPedidoFaker.ObterFakeToMock();
            var usuariosFake = AuxUsuarioFaker.ObterUsuariosFakeToMock();

            var valorEsperado = listPedidoFake.Where(l => l.Id == pedidoFake.Id).Count();

            //Act
            _repositoryMock.Setup(r => r.GetAll(default)).ReturnsAsync(listPedidoFake);
            _senderMock.Setup(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default)).ReturnsAsync(usuariosFake);

            var resultado = await pedidoHandler.Handle(new PedidoCommandGetAll(string.Empty, pedidoFake.Id, EStatusPedido.Todos), default);
            var valorRetornado = resultado.Count();

            //Assert
            Assert.NotNull(resultado);
            Assert.Equal(valorRetornado, valorEsperado);
            _repositoryMock.Verify(r => r.GetAll(default), Times.Once);
            _senderMock.Verify(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default), Times.Never);
        }

        [Fact]
        public async Task ListarPedidosAsync_DeveRetornarListaPedidos_ComFiltroStatus()
        {
            //Arrange
            var listPedidoFake = AuxPedidoFaker.ObterFakesToMock();
            var pedidoFake = AuxPedidoFaker.ObterFakeToMock();
            var usuariosFake = AuxUsuarioFaker.ObterUsuariosFakeToMock();

            var valorEsperado = listPedidoFake.Where(l => l.Status == pedidoFake.Status).Count();

            //Act
            _repositoryMock.Setup(r => r.GetAll(default)).ReturnsAsync(listPedidoFake);
            _senderMock.Setup(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default)).ReturnsAsync(usuariosFake);

            var resultado = await pedidoHandler.Handle(new PedidoCommandGetAll(string.Empty, null, pedidoFake.Status), default);
            var valorRetornado = resultado.Count();

            //Assert
            Assert.NotNull(resultado);
            Assert.Equal(valorRetornado, valorEsperado);
            _repositoryMock.Verify(r => r.GetAll(default), Times.Once);
            _senderMock.Verify(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default), Times.Never);
        }

        [Fact]
        public async Task ListarPedidosAsync_DeveRetornarListaPedidos_ComTodosOsFiltros()
        {
            //Arrange
            var listPedidoFake = AuxPedidoFaker.ObterFakesToMock();
            var pedidoFake = AuxPedidoFaker.ObterFakeToMock();
            var usuariosFake = AuxUsuarioFaker.ObterUsuariosFakeToMock();
            var usuarioFake = AuxUsuarioFaker.ObterUsuarioFakeToMock(1);

            var valorEsperado = listPedidoFake
                .Where(l => l.ClienteId == usuarioFake.Id)
                .Where(l => l.Id == pedidoFake.Id)
                .Where(l => l.Status == pedidoFake.Status).Count();

            //Act
            _repositoryMock.Setup(r => r.GetAll(default)).ReturnsAsync(listPedidoFake);
            _senderMock.Setup(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default)).ReturnsAsync(usuariosFake);

            var resultado = await pedidoHandler.Handle(new PedidoCommandGetAll(usuarioFake.Nome, pedidoFake.Id, pedidoFake.Status), default);
            var valorRetornado = resultado.Count();

            //Assert
            Assert.NotNull(resultado);
            Assert.Equal(valorRetornado, valorEsperado);
            _repositoryMock.Verify(r => r.GetAll(default), Times.Once);
            _senderMock.Verify(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default), Times.Once);
        }

        [Fact]
        public async Task ObterPedidoPeloIdAsync_DeveRetornarPedido_QuandoEncontrado()
        {
            //Arrange
            var pedidoFake = AuxPedidoFaker.ObterFakeToMock();
            var valorEsperado = pedidoFake;

            //Act
            _repositoryMock.Setup(r => r.GetById(pedidoFake.Id, default)).ReturnsAsync(pedidoFake);

            var resultado = await pedidoHandler.Handle(new PedidoCommandGetById(pedidoFake.Id), default);
            var valorRetornado = resultado;

            //Assert
            Assert.NotNull(resultado);
            Assert.Equal(valorEsperado, valorRetornado);
            _repositoryMock.Verify(r => r.GetById(pedidoFake.Id, default), Times.Once);
        }

        [Fact]
        public async Task ObterPedidoPeloIdAsync_DeveRetornarDefault_QuandoNaoEncontrado()
        {
            //Arrange
            var pedidoFake = AuxPedidoFaker.ObterFakeToMock(false);
            var valorEsperado = default(Pedido?);

            //Act
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>(), default)).ReturnsAsync(default(Pedido?));

            var resultado = await pedidoHandler.Handle(new PedidoCommandGetById(pedidoFake.Id), default);
            var valorRetornado = resultado;

            //Assert
            Assert.Null(resultado);
            Assert.Equal(valorEsperado, valorRetornado);
            _repositoryMock.Verify(r => r.GetById(pedidoFake.Id, default), Times.Once);
        }

        [Fact]
        public async Task RegistrarPedidoAsync_DeveRetornarNovoId()
        {
            //Arrange
            var commandVenda = new PedidoCommandVenda() { Itens = [] };
            var fake = AuxPedidoFaker.ObterFakeToMock(false);
            var listFake = AuxPedidoFaker.ObterFakesToMock();
            var listFakeComNovoInsert = listFake.Concat([fake]);
            var valorEsperado = fake.Id;

            //Act
            _mapperMock.Setup(m => m.ToEntity(It.IsAny<PedidoCommandInsert>())).Returns(fake);
            _repositoryMock.Setup(r => r.Insert(It.IsAny<Pedido>(), default)).ReturnsAsync(fake.Id);
            _dbcotextMock.Setup(d => d.SaveChangesAsync(default)).ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAll(default)).ReturnsAsync(listFakeComNovoInsert);
            _senderMock.Setup(s => s.Send(It.IsAny<PedidoItemCommandInsertList>(), default)).ReturnsAsync([]);

            var resultado = await pedidoHandler.Handle(commandVenda, default);
            var valorRetornado = resultado;

            //Assert
            Assert.NotNull(resultado);
            Assert.Equal(valorEsperado, valorRetornado);
            _mapperMock.Verify(r => r.ToEntity(It.IsAny<PedidoCommandInsert>()), Times.Once);
            _repositoryMock.Verify(r => r.Insert(It.IsAny<Pedido>(), default), Times.Once);
            _dbcotextMock.Verify(d => d.SaveChangesAsync(default), Times.Once);
            _repositoryMock.Verify(r => r.GetAll(default), Times.Once);
            _senderMock.Verify(r => r.Send(It.IsAny<PedidoItemCommandInsertList>(), default), Times.Once);
        }

        [Fact]
        public async Task CancelaPedidoAsync_DeveRetornarTrue_QuandoPedidoNaoFoiPago()
        {
            //Arrange
            var pedidoFake = AuxPedidoFaker.ObterFakeToMock(false)!;
            var commandCancelar = new PedidoCommandCancelar(pedidoFake.Id);
            var valorEsperado = true;

            //Act
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>(), default)).ReturnsAsync(pedidoFake);
            _repositoryMock.Setup(r => r.Update(It.IsAny<Pedido>(), default)).ReturnsAsync(true);
            _dbcotextMock.Setup(d => d.SaveChangesAsync(default)).ReturnsAsync(1);

            var resultado = await pedidoHandler.Handle(commandCancelar, default);
            var valorRetornado = resultado;

            //Assert
            Assert.Equal(valorEsperado, valorRetornado);
            _repositoryMock.Verify(r => r.GetById(It.IsAny<int>(), default), Times.Once);
            _repositoryMock.Verify(r => r.Update(It.IsAny<Pedido>(), default), Times.Once);
            _dbcotextMock.Verify(d => d.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task CancelaPedidoAsync_DeveRetornarFalse_QuandoPedidoAprovadoOuJaCancelado()
        {
            //Arrange
            var status = (EStatusPedido)(new Random()).Next(1);
            var pedidoFake = AuxPedidoFaker.ObterFakeToMock(status: status)!;
            var commandCancelar = new PedidoCommandCancelar(pedidoFake.Id);
            var valorEsperado = false;

            //Act
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>(), default)).ReturnsAsync(pedidoFake);
            _repositoryMock.Setup(r => r.Update(It.IsAny<Pedido>(), default)).ReturnsAsync(true);
            _dbcotextMock.Setup(d => d.SaveChangesAsync(default)).ReturnsAsync(1);

            var resultado = await pedidoHandler.Handle(commandCancelar, default);
            var valorRetornado = resultado;

            //Assert
            Assert.Equal(valorEsperado, valorRetornado);
            _repositoryMock.Verify(r => r.GetById(It.IsAny<int>(), default), Times.Once);
            _repositoryMock.Verify(r => r.Update(It.IsAny<Pedido>(), default), Times.Never);
            _dbcotextMock.Verify(d => d.SaveChangesAsync(default), Times.Never);
        }

        [Fact]
        public async Task AprovaPedidoAsync_DeveRetornarTrue_QuandoPedidoNaoFoiPago()
        {
            //Arrange
            var pedidoFake = AuxPedidoFaker.ObterFakeToMock(false)!;
            var commandAprovar = new PedidoCommandAprovar(pedidoFake.Id);
            var valorEsperado = true;

            //Act
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>(), default)).ReturnsAsync(pedidoFake);
            _repositoryMock.Setup(r => r.Update(It.IsAny<Pedido>(), default)).ReturnsAsync(true);
            _dbcotextMock.Setup(d => d.SaveChangesAsync(default)).ReturnsAsync(1);

            var resultado = await pedidoHandler.Handle(commandAprovar, default);
            var valorRetornado = resultado;

            //Assert
            Assert.Equal(valorEsperado, valorRetornado);
            _repositoryMock.Verify(r => r.GetById(It.IsAny<int>(), default), Times.Once);
            _repositoryMock.Verify(r => r.Update(It.IsAny<Pedido>(), default), Times.Once);
            _dbcotextMock.Verify(d => d.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task AprovaPedidoAsync_DeveRetornarFalse_QuandoPedidoAprovadoOuJaCancelado()
        {
            //Arrange
            var status = (EStatusPedido)(new Random()).Next(1);
            var pedidoFake = AuxPedidoFaker.ObterFakeToMock(status: status)!;
            var commandAprovar = new PedidoCommandCancelar(pedidoFake.Id);
            var valorEsperado = false;

            //Act
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>(), default)).ReturnsAsync(pedidoFake);
            _repositoryMock.Setup(r => r.Update(It.IsAny<Pedido>(), default)).ReturnsAsync(true);
            _dbcotextMock.Setup(d => d.SaveChangesAsync(default)).ReturnsAsync(1);

            var resultado = await pedidoHandler.Handle(commandAprovar, default);
            var valorRetornado = resultado;

            //Assert
            Assert.Equal(valorEsperado, valorRetornado);
            _repositoryMock.Verify(r => r.GetById(It.IsAny<int>(), default), Times.Once);
            _repositoryMock.Verify(r => r.Update(It.IsAny<Pedido>(), default), Times.Never);
            _dbcotextMock.Verify(d => d.SaveChangesAsync(default), Times.Never);
        }
    }
}