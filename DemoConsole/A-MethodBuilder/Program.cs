namespace DemoConsole.A_MethodBuilder;

public static class Program
{
    public static async Task Main()
    {
        WriteLine();

        int delays = await CountDelays();
        WriteLine($"Delays: {delays}");
    }

    // FEATURE: Allow [AsyncMethodBuilder(...)] on methods
    [AsyncMethodBuilder(typeof(DelayCountMethodBuilder<>))]
    public static async Task<int> CountDelays()
    {
        await Task.Delay(10);
        await new HttpClient().GetAsync("https://www.teamrockstars.nl/");
        await Task.Delay(10);
        await Task.Delay(10);
        return 0;
    }

    #region Way too much boilerplate

    internal class DelayCountMethodBuilder<TResult>
    {
        private readonly TaskCompletionSource<TResult> _taskSource = new();
        private int _delayCount;
        private IAsyncStateMachine? _stateMachine;

        public Task<TResult> Task => _taskSource.Task;

        public static DelayCountMethodBuilder<TResult> Create() => new DelayCountMethodBuilder<TResult>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            _stateMachine = stateMachine;
            _stateMachine.MoveNext();
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void SetResult(TResult result)
        {
            _taskSource.SetResult((TResult)(object)_delayCount);
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            bool isDelayPromise = awaiter.GetType()
                // Ahum, don't use this in production, ok?
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)[0]
                .GetValue(awaiter)!
                .GetType().Name == "DelayPromise";

            awaiter.OnCompleted(() =>
            {
                if (isDelayPromise)
                {
                    _delayCount++;
                }
                _stateMachine?.MoveNext();
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            AwaitOnCompleted(ref awaiter, ref stateMachine);
        }

        public void SetException(Exception exception)
        {
            _taskSource.SetException(exception);
        }
    }

    #endregion
}
