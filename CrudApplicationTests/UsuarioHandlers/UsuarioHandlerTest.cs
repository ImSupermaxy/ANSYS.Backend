using ANSYS.Application.Global.Usuarios.Commands;
using ANSYS.Application.Global.Usuarios.Handlers;
using ANSYS.Domain.Abstractions.Context.EntityFramework;
using ANSYS.Domain.Abstractions.Mappers;
using ANSYS.Domain.Global.Usuarios.Entities;
using ANSYS.Domain.Global.Usuarios.Repositories;
using MediatR;
using Moq;

namespace CrudApplicationTests.UsuarioHandlers
{
    public class UsuarioHandlerTest
    {
        private readonly Mock<IEntityFrameworkDBContext> _dbcotextMock;
        private readonly Mock<IUsuarioRepository> _repositoryMock;
        private readonly Mock<IEntityMapper<Usuario, UsuarioCommandInsert, UsuarioCommandUpdate>> _mapperMock;
        private readonly Mock<ISender> _senderMock;
        private readonly UsuarioHandler usuarioHandler;

        public UsuarioHandlerTest()
        {
            _dbcotextMock = new Mock<IEntityFrameworkDBContext>();
            _repositoryMock = new Mock<IUsuarioRepository>();
            _mapperMock = new Mock<IEntityMapper<Usuario, UsuarioCommandInsert, UsuarioCommandUpdate>>();
            _senderMock = new Mock<ISender>();

            usuarioHandler = new UsuarioHandler(
                _dbcotextMock.Object,
                _repositoryMock.Object,
                _mapperMock.Object,
                _senderMock.Object);
        }

        [Fact]
        public async Task ListarUsuariosAsync_DeveRetornarListaDeUsuarios()
        {
            //Arrange
            var listUsuarioFake = AuxUsuarioFaker.ObterUsuariosFakeToMock();
            var valorEsperado = listUsuarioFake.Count();

            //Act
            _repositoryMock.Setup(r => r.GetAll(default)).ReturnsAsync(listUsuarioFake);

            var resultado = await usuarioHandler.Handle(new UsuarioCommandGetAll(), default);
            var valorRetornado = resultado.Count();

            //Assert
            Assert.NotNull(resultado);
            Assert.Equal(valorRetornado, valorEsperado);
            _repositoryMock.Verify(r => r.GetAll(default), Times.Once);
        }

        [Fact]
        public async Task ObterUsuarioPeloIdAsync_DeveRetornarUsuario_QuandoEncontrado()
        {
            //Arrange
            var usuarioFake = AuxUsuarioFaker.ObterUsuarioFakeToMock();
            var valorEsperado = usuarioFake;

            //Act
            _repositoryMock.Setup(r => r.GetById(usuarioFake.Id, default)).ReturnsAsync(usuarioFake);

            var resultado = await usuarioHandler.Handle(new UsuarioCommandGetById(usuarioFake.Id), default);
            var valorRetornado = resultado;

            //Assert
            Assert.NotNull(resultado);
            Assert.Equal(valorEsperado, valorRetornado);
            _repositoryMock.Verify(r => r.GetById(usuarioFake.Id, default), Times.Once);
        }

        [Fact]
        public async Task ObterUsuarioPeloIdAsync_DeveRetornarDefault_QuandoNaoEncotnrado()
        {
            //Arrange
            var usuarioFake = AuxUsuarioFaker.ObterUsuarioFakeToMock(false);
            var valorEsperado = default(Usuario?);

            //Act
            _repositoryMock.Setup(r => r.GetById(usuarioFake.Id, default)).ReturnsAsync(default(Usuario?));

            var resultado = await usuarioHandler.Handle(new UsuarioCommandGetById(usuarioFake.Id), default);
            var valorRetornado = resultado;

            //Assert
            Assert.Null(resultado);
            Assert.Equal(valorEsperado, valorRetornado);
            _repositoryMock.Verify(r => r.GetById(usuarioFake.Id, default), Times.Once);
        }

        [Fact]
        public async Task RegistrarUsuarioAsync_DeveRetornarNovoId_QuandoEmailNaoExiste()
        {
            //Arrange
            var commandInsert = new UsuarioCommandInsert();
            var usuarioFake = AuxUsuarioFaker.ObterUsuarioFakeToMock(false);
            var listUsuarioFake = AuxUsuarioFaker.ObterUsuariosFakeToMock();
            var usuariosFakeComNovoInsert = listUsuarioFake.Concat([usuarioFake]);
            var valorEsperado = usuarioFake.Id;

            //Act
            _mapperMock.Setup(m => m.ToEntity(commandInsert)).Returns(usuarioFake);
            _senderMock.Setup(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default)).ReturnsAsync(listUsuarioFake);
            _repositoryMock.Setup(r => r.Insert(usuarioFake, default)).ReturnsAsync(usuarioFake.Id);
            _repositoryMock.Setup(r => r.GetAll(default)).ReturnsAsync(usuariosFakeComNovoInsert);
            _dbcotextMock.Setup(d => d.SaveChangesAsync(default)).ReturnsAsync(1);

            var resultado = await usuarioHandler.Handle(commandInsert, default);
            var valorRetornado = resultado;

            //Assert
            Assert.NotNull(resultado);
            Assert.Equal(valorEsperado, valorRetornado);
        }

        [Fact]
        public async Task RegistrarUsuarioAsync_DeveRetornarNull_QuandoEmailJaExiste()
        {
            //Arrange
            var usuarioFake = AuxUsuarioFaker.ObterUsuarioFakeToMock(true);
            var commandInsert = new UsuarioCommandInsert() { Nome = usuarioFake.Nome, Email = usuarioFake.Email };
            var listUsuarioFake = AuxUsuarioFaker.ObterUsuariosFakeToMock();
            var listUsuarioFakeComNovoInsert = listUsuarioFake.Concat([usuarioFake]);
            int? valorEsperado = null;

            //Act
            _mapperMock.Setup(m => m.ToEntity(commandInsert)).Returns(usuarioFake);
            _senderMock.Setup(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default)).ReturnsAsync(listUsuarioFake);
            _repositoryMock.Setup(r => r.Insert(usuarioFake, default)).ReturnsAsync(usuarioFake.Id);
            _repositoryMock.Setup(r => r.GetAll(default)).ReturnsAsync(listUsuarioFakeComNovoInsert);
            _dbcotextMock.Setup(d => d.SaveChangesAsync(default)).ReturnsAsync(1);

            var resultado = await usuarioHandler.Handle(commandInsert, default);
            var valorRetornado = resultado;

            //Assert
            Assert.Null(resultado);
            Assert.Equal(valorEsperado, valorRetornado);
        }

        [Fact]
        public async Task AtualizarUsuarioAsync_DeveRetornarTrue_QuandoEmailNaoExiste()
        {
            //Arrange
            var commandUpdate = new UsuarioCommandUpdate();
            var usuarioInseridoFake = AuxUsuarioFaker.ObterUsuarioFakeToMock(true)!;
            var usuarioFake = AuxUsuarioFaker.ObterUsuarioFakeToMock(usuarioInseridoFake, email: "NovoEmail@teste.com");
            var listUsuarioFake = AuxUsuarioFaker.ObterUsuariosFakeToMock();
            var valorEsperado = true;

            //Act
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>(), default)).ReturnsAsync(usuarioInseridoFake);
            _senderMock.Setup(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default)).ReturnsAsync(listUsuarioFake);
            _mapperMock.Setup(m => m.ToEntity(It.IsAny<UsuarioCommandUpdate>(), It.IsAny<Usuario>())).Returns(usuarioFake);
            _repositoryMock.Setup(r => r.Update(It.IsAny<Usuario>(), default)).ReturnsAsync(true);
            _dbcotextMock.Setup(d => d.SaveChangesAsync(default)).ReturnsAsync(1);

            var resultado = await usuarioHandler.Handle(commandUpdate, default);
            var valorRetornado = resultado;

            //Assert
            Assert.NotNull(resultado);
            Assert.Equal(valorEsperado, valorRetornado);
        }

        [Fact]
        public async Task AtualizarUsuarioAsync_DeveRetornarNull_QuandoEmailJaExiste()
        {
            //Arrange
            var commandUpdate = new UsuarioCommandUpdate();
            var usuarioInseridoFake = AuxUsuarioFaker.ObterUsuarioFakeToMock(true)!;
            var usuarioFake = AuxUsuarioFaker.ObterUsuarioFakeToMock(usuarioInseridoFake);
            var listUsuarioFake = AuxUsuarioFaker.ObterUsuariosFakeToMock();
            var commandGetAll = new UsuarioCommandGetAll();
            bool? valorEsperado = null;

            //Act
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>(), default)).ReturnsAsync(usuarioInseridoFake);
            _senderMock.Setup(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default)).ReturnsAsync(listUsuarioFake);
            _mapperMock.Setup(m => m.ToEntity(It.IsAny<UsuarioCommandUpdate>(), It.IsAny<Usuario>())).Returns(usuarioFake);
            _repositoryMock.Setup(r => r.Update(It.IsAny<Usuario>(), default)).ReturnsAsync(true);
            _dbcotextMock.Setup(d => d.SaveChangesAsync(default)).ReturnsAsync(1);

            var resultado = await usuarioHandler.Handle(commandUpdate, default);
            var valorRetornado = resultado;

            //Assert
            Assert.Null(resultado);
            Assert.Equal(valorEsperado, valorRetornado);
            _repositoryMock.Verify(r => r.GetById(It.IsAny<int>(), default), Times.Once);
            _senderMock.Verify(r => r.Send(It.IsAny<UsuarioCommandGetAll>(), default), Times.Once);
            _mapperMock.Verify(r => r.ToEntity(It.IsAny<UsuarioCommandUpdate>(), It.IsAny<Usuario>()), Times.Once);
            _repositoryMock.Verify(r => r.Update(It.IsAny<Usuario>(), default), Times.Never);
            _dbcotextMock.Verify(d => d.SaveChangesAsync(default), Times.Never);
        }

        [Fact]
        public async Task AtualizarUsuarioAsync_DeveRetornarFalse_QuandoUsuarioNaoEncontrado()
        {
            //Arrange
            var commandUpdate = new UsuarioCommandUpdate();
            var usuarioInseridoFake = AuxUsuarioFaker.ObterUsuarioFakeToMock(true)!;
            var usuarioFake = AuxUsuarioFaker.ObterUsuarioFakeToMock(usuarioInseridoFake, id: -1);
            var listUsuarioFake = AuxUsuarioFaker.ObterUsuariosFakeToMock();
            var commandGetAll = new UsuarioCommandGetAll();
            bool? valorEsperado = false;

            //Act
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>(), default)).ReturnsAsync(default(Usuario?));
            _senderMock.Setup(s => s.Send(It.IsAny<UsuarioCommandGetAll>(), default)).ReturnsAsync(listUsuarioFake);
            _mapperMock.Setup(m => m.ToEntity(It.IsAny<UsuarioCommandUpdate>(), It.IsAny<Usuario>())).Returns(usuarioFake);
            _repositoryMock.Setup(r => r.Update(It.IsAny<Usuario>(), default)).ReturnsAsync(false);
            _dbcotextMock.Setup(d => d.SaveChangesAsync(default)).ReturnsAsync(1);

            var resultado = await usuarioHandler.Handle(commandUpdate, default);
            var valorRetornado = resultado;

            //Assert
            Assert.NotNull(resultado);
            Assert.Equal(valorEsperado, valorRetornado);
            _repositoryMock.Verify(r => r.GetById(It.IsAny<int>(), default), Times.Once);
            _senderMock.Verify(r => r.Send(It.IsAny<UsuarioCommandGetAll>(), default), Times.Never);
            _mapperMock.Verify(r => r.ToEntity(It.IsAny<UsuarioCommandUpdate>(), It.IsAny<Usuario>()), Times.Never);
            _repositoryMock.Verify(r => r.Update(It.IsAny<Usuario>(), default), Times.Never);
            _dbcotextMock.Verify(d => d.SaveChangesAsync(default), Times.Never);
        }
    }
}