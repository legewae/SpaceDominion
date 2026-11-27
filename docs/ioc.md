# IoC-контейнер

## Что используется

Эталонный репозиторий и лекции используют контейнер преподавателя — две сборки:

| Сборка | Пространство имён | Что даёт |
|---|---|---|
| `Common/core.dll` (v0.1.0.0) | `App` | `App.Ioc.Resolve<T>(string name, params object[] args)`, `App.ICommand` |
| `Common/scopes.dll` (v1.0.0.0) | `App.Scopes` | `InitCommand`, `RegisterDependencyCommand`, `SetCurrentScopeCommand`, `ClearCurrentScopeCommand`, `DependencyResolver` |

Обе подключены как `<Reference>` с `HintPath` в `..\Common\`, без NuGet-пакетов.

## Как зарегистрировать зависимость

Регистрация — тоже команда:

```csharp
Ioc.Resolve<App.ICommand>(
    "IoC.Register",
    "Commands.Send",
    (object[] args) => new SendCommand((ICommand)args[0], (ICommandReceiver)args[1])
).Execute();
```

Разрешение:

```csharp
var command = Ioc.Resolve<ICommand>("Commands.Send", command, receiver);
```

## Скоупы

```csharp
new App.Scopes.InitCommand().Execute();
var scope = Ioc.Resolve<object>("IoC.Scope.Create");
Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
```

`Scope` — набор зависимостей, разрешаемых в рамках матча; переключается на время `Execute()`.

## Правила

1. `new` — только внутри стратегий (лямбд регистрации) и фабрик.
2. Доменный код не вызывает `Ioc.Resolve` — зависимости приходят через конструктор.
3. Имена зависимостей: `Commands.<Имя>`, `Adapters.<Имя>`, `Handlers.<Событие>`, `Properties.<Свойство>`.
4. Стратегии разрешения не используют рефлексию; рефлексия допустима только при генерации стратегий/адаптеров на старте.

## Открытый вопрос

Поведение `App.Scopes` при нескольких потоках пока не проверено — обязательный спайк в PR-06
(два потока, два текущих скоупа). Если текущий скоуп «протекает», вводится обёртка на
`ThreadLocal`/`AsyncLocal`, решение фиксируется в ADR.
