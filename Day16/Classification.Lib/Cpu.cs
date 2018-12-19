using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Lib
{
    public class Cpu
    {
        private int[] _registers = { 0, 0, 0, 0 };
        private Dictionary<string, Operator> _operations = new Dictionary<string, Operator>();
        private Dictionary<int, Operator> _opsByOpCode = new Dictionary<int, Operator>();

        public Cpu() { }
        public Cpu(int[] initialState)
        {
            if (initialState.Length > 4) throw new ArgumentException("CPU cannot handle states with more than 4 registers");
            _registers = initialState;
            LoadOperations();
        }

        public void ExecuteInstruction(int[] i)
        {
            var op = _opsByOpCode[i[0]];
            if (op is null)
            {
                throw new NullReferenceException($"No valid OpCode in instruction [{i[0]}, {i[1]}, {i[2]}, {i[3]}]");
            } else
            {
                ExecuteInstruction(i, op);
            }
        }
        public void ExecuteInstruction(int[] instruction, Operator op)
        {
            op.Execute(instruction, out var after);
            _registers = after;
        }
        public int[] TryExecuteInstruction(int[] registers, int[] instruction, Operator op)
        {
            op.TryExecute(registers, instruction, out var after);
            return after;
        }
        public int[] GetRegisters()
        {
            return _registers;
        }
        public Dictionary<string, Operator> Operations
        {
            get
            {
                return _operations;
            }
        }


        private void LoadOperations()
        {
            var opTypes = FindOperationTypes(typeof(Operator));
            int[] copyOfRegisters = new int[] {0, 0, 0, 0};
            foreach (var opType in opTypes)
            {
                if (opType.IsSubclassOf(typeof(Operator)) && !opType.Equals(typeof(Operator)))
                {
                    Operator newInstance = (Operator)Activator.CreateInstance(opType, args: copyOfRegisters);
                    _operations.Add(newInstance.opId, newInstance);
                    Console.WriteLine($"OpCode [{newInstance.OpCode}]  for op [{newInstance.opId}]");
                    _opsByOpCode.Add(newInstance.OpCode, newInstance);
                }
            }
        }

        private int[] MakeCopyOfCurrentRegisters()
        {
            int[] copyOfRegisters = { _registers[0], _registers[1], _registers[2], _registers[3] };
            return copyOfRegisters;
        }

        private IEnumerable<Type> FindOperationTypes(Type baseType)
        {
            return baseType.Assembly.GetTypes().Where(t => baseType.IsAssignableFrom(t));
        }
    }
}
