namespace SpaceDominion.Lib.Commands;

// Единая абстракция действия в игре.
// Наследует контракт контейнера зависимостей (App.ICommand), поэтому одна и та же
// команда подходит и домену, и регистрации в IoC: второго интерфейса не заводим.
public interface ICommand : App.ICommand
{
}
