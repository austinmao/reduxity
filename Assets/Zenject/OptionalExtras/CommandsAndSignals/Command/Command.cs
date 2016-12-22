using System;
using ModestTree;

namespace Zenject
{
    public interface ICommand
    {
    }

    // Zero params
    [ZenjectAllowDuringValidationAttribute]
    public abstract class Command : ICommand
    {
        Action _handler;

        [Inject]
        public void Construct(Action handler, DiContainer container)
        {
            _handler = handler;

            if (container.IsValidating)
            // During validation, we want to instantiate whatever class we're calling immediately
            {
                handler();
            }
        }

        public void Execute()
        {
            _handler();
        }
    }

    // One param
    [ZenjectAllowDuringValidationAttribute]
    public abstract class Command<TParam1> : ICommand
    {
        Action<TParam1> _handler;

        [Inject]
        public void Construct(Action<TParam1> handler, DiContainer container)
        {
            _handler = handler;

            if (container.IsValidating)
            // During validation, we want to instantiate whatever class we're calling immediately
            {
                handler(default(TParam1));
            }
        }

        public void Execute(TParam1 param1)
        {
            _handler(param1);
        }
    }

    // Two params
    [ZenjectAllowDuringValidationAttribute]
    public abstract class Command<TParam1, TParam2> : ICommand
    {
        Action<TParam1, TParam2> _handler;

        [Inject]
        public void Construct(Action<TParam1, TParam2> handler, DiContainer container)
        {
            _handler = handler;

            if (container.IsValidating)
            // During validation, we want to instantiate whatever class we're calling immediately
            {
                handler(default(TParam1), default(TParam2));
            }
        }

        public void Execute(TParam1 param1, TParam2 param2)
        {
            _handler(param1, param2);
        }
    }

    // Three params
    [ZenjectAllowDuringValidationAttribute]
    public abstract class Command<TParam1, TParam2, TParam3> : ICommand
    {
        Action<TParam1, TParam2, TParam3> _handler;

        [Inject]
        public void Construct(Action<TParam1, TParam2, TParam3> handler, DiContainer container)
        {
            _handler = handler;

            if (container.IsValidating)
            // During validation, we want to instantiate whatever class we're calling immediately
            {
                handler(default(TParam1), default(TParam2), default(TParam3));
            }
        }

        public void Execute(TParam1 param1, TParam2 param2, TParam3 param3)
        {
            _handler(param1, param2, param3);
        }
    }

    // Four params
    [ZenjectAllowDuringValidationAttribute]
    public abstract class Command<TParam1, TParam2, TParam3, TParam4> : ICommand
    {
        Action<TParam1, TParam2, TParam3, TParam4> _handler;

        [Inject]
        public void Construct(Action<TParam1, TParam2, TParam3, TParam4> handler, DiContainer container)
        {
            _handler = handler;

            if (container.IsValidating)
            // During validation, we want to instantiate whatever class we're calling immediately
            {
                handler(default(TParam1), default(TParam2), default(TParam3), default(TParam4));
            }
        }

        public void Execute(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            _handler(param1, param2, param3, param4);
        }
    }

    // Five params
    [ZenjectAllowDuringValidationAttribute]
    public abstract class Command<TParam1, TParam2, TParam3, TParam4, TParam5> : ICommand
    {
        ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5> _handler;

        [Inject]
        public void Construct(ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5> handler, DiContainer container)
        {
            _handler = handler;

            if (container.IsValidating)
            // During validation, we want to instantiate whatever class we're calling immediately
            {
                handler(default(TParam1), default(TParam2), default(TParam3), default(TParam4), default(TParam5));
            }
        }

        public void Execute(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            _handler(param1, param2, param3, param4, param5);
        }
    }

    // Six params
    [ZenjectAllowDuringValidationAttribute]
    public abstract class Command<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> : ICommand
    {
        ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> _handler;

        [Inject]
        public void Construct(ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> handler, DiContainer container)
        {
            _handler = handler;

            if (container.IsValidating)
            // During validation, we want to instantiate whatever class we're calling immediately
            {
                handler(default(TParam1), default(TParam2), default(TParam3), default(TParam4), default(TParam5), default(TParam6));
            }
        }

        public void Execute(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
        {
            _handler(param1, param2, param3, param4, param5, param6);
        }
    }
}
