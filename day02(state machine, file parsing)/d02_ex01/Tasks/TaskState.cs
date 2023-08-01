namespace d02_ex01.Tasks
{
    public class TaskState
    {
        public bool SetDone() => _state.SetDone(this);
        public bool SetIrrelevant() => _state.SetIrrelevant(this);
    
        private State _state = new New();
        private void SetState(State state) => _state = state;
    
        abstract class State
        {
            public virtual string Title => "Abstract State";
            public virtual bool SetDone(TaskState taskState) => false;
            public virtual bool SetIrrelevant(TaskState taskState)=> false;
        }
        private class New : State
        {
            public override string Title => "New";

            public override bool SetDone(TaskState taskState)
            {
                taskState.SetState(new Completed());
                return true;
            }

            public override bool SetIrrelevant(TaskState taskState)
            {
                taskState.SetState(new Irrelevant());
                return true;
            }
        }
        private class Completed : State
        {
            public override string Title => "Done";

        }
        private class Irrelevant : State
        {
            public override string Title => "Wontdo";

        }

        public override string ToString() => _state.Title;
    }
}