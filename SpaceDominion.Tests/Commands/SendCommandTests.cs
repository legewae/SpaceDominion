using Moq;
using SpaceDominion.Lib.Commands;

namespace SpaceDominion.Tests.Commands;

public class SendCommandTests
{
    [Fact]
    public void Execute_SendsWrappedCommandToReceiver()
    {
        var command = new Mock<ICommand>();
        var receiver = new Mock<ICommandReceiver>();
        var sut = new SendCommand(command.Object, receiver.Object);

        sut.Execute();

        receiver.Verify(r => r.Receive(command.Object), Times.Once);
    }

    [Fact]
    public void Execute_WhenReceiverThrows_PropagatesException()
    {
        var receiver = new Mock<ICommandReceiver>();
        receiver
            .Setup(r => r.Receive(It.IsAny<ICommand>()))
            .Throws(new InvalidOperationException("queue is full"));
        var sut = new SendCommand(new Mock<ICommand>().Object, receiver.Object);

        var act = () => sut.Execute();

        var exception = Assert.Throws<InvalidOperationException>(act);
        Assert.Equal("queue is full", exception.Message);
    }

    [Fact]
    public void Constructor_WhenCommandIsNull_Throws()
    {
        var receiver = new Mock<ICommandReceiver>();

        var act = () => new SendCommand(null!, receiver.Object);

        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void Constructor_WhenReceiverIsNull_Throws()
    {
        var command = new Mock<ICommand>();

        var act = () => new SendCommand(command.Object, null!);

        Assert.Throws<ArgumentNullException>(act);
    }
}
