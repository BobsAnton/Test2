using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test2.Models
{
    public static class Expression
    {
        private static List<string> operators = new List<string>(new string[] { "(", ")", "+", "-", "*", "/" });

        public static int Compute(string expression)
        {
            try
            {
                // Перевод в обратную польскую запись.
                var queue = new Queue<string>(ToRPN(expression));
                
                // Берем из очереди первый символ.
                string s = queue.Dequeue();
                var stack = new Stack<string>();
                while (queue.Count >= 0)
                {
                    if (!operators.Contains(s))
                    {
                        // Если символ не является операцией, то добавляем его в стек.
                        stack.Push(s);
                        // Берем следующий символ из очереди.
                        s = queue.Dequeue();
                    }
                    else
                    {
                        // Если символ является операцией, то выполняем ее над
                        // двумя последними числами из стека.
                        var ans = 0;
                        var x = int.Parse(stack.Pop());
                        var y = int.Parse(stack.Pop());
                        switch (s)
                        {
                            case "+":
                                {
                                    ans = x + y;
                                    break;
                                }
                            case "-":
                                {
                                    ans = y - x;
                                    break;
                                }
                            case "*":
                                {
                                    ans = y * x;
                                    break;
                                }
                            case "/":
                                {
                                    ans = y / x;
                                    break;
                                }
                        }
                        // Возвращаем результат в стек.
                        stack.Push(ans.ToString());

                        // Берем следующий символ из очереди.
                        if (queue.Count > 0)
                            s = queue.Dequeue();
                        else
                            break;
                    }

                }

                // Возвращаем результат.
                return int.Parse(stack.Pop());
            }
            catch
            {
                // Если есть ошибки, то возвращаем ноль.
                try
                {
                    return int.Parse(expression);
                }
                catch
                {
                    return 0;
                }
            }
        }

        private static IEnumerable<string> ToRPN(string expression)
        {
            // Необходимо создать очередь строк (ОПЗ).
            var RPN = new List<string>();
            var stack = new Stack<string>();

            // Перебираем все части строки (скобки, операции, числа).
            foreach (var c in Separate(expression))
            {
                if (operators.Contains(c))
                {
                    if (stack.Count > 0 && !c.Equals("("))
                    {
                        // Если символ не число.
                        if (c.Equals(")"))
                        {
                            // Если символ ), то вытаскиваем символ из стека.
                            string s = stack.Pop();
                            while (s != "(")
                            {
                                // Пока не встретим символ (, вытаскиваем все 
                                // символы в искомую очередь.
                                RPN.Add(s);
                                s = stack.Pop();
                            }
                        }
                        else if (Priority(c) > Priority(stack.Peek()))
                            // Если символ явлается операцией и операция
                            // по приоритету выше операции в стеке
                            // то добавляем текущий символ в стек.
                            stack.Push(c);
                        else
                        {
                            // Если символ явлается операцией и операция
                            // по приоритету меньше или равен операции в стеке, то.
                            while (stack.Count > 0 && Priority(c) <= Priority(stack.Peek()))
                                // Пока стек не пуст, добавляем операции из стека
                                // в искомую очередь.
                                RPN.Add(stack.Pop());

                            // Добавляем текущий символ в стек.
                            stack.Push(c);
                        }
                    }
                    else
                        // Если стек пуст или символ является (, 
                        // то добавляем текущий символ в стек.
                        stack.Push(c);
                }
                else
                    // Если символ является числом, 
                    // то добавляем его в искомую очередь.
                    RPN.Add(c);
            }
            // Если в стеке остались символы,
            // то добавляем их в искомую очередь.
            if (stack.Count > 0)
                foreach (var c in stack)
                    RPN.Add(c);

            // Возвращаем искомую очередь.
            return RPN;
        }

        private static IEnumerable<string> Separate(string expression)
        {
            // Выполняется на каждой итерации.
            var i = 0;
            while (i < expression.Length)
            {
                // Получаем очередной символ в строке.
                var s = expression[i].ToString();
                if (!operators.Contains(expression[i].ToString()))
                {
                    // Если символ является числом, то добавляем 
                    // следующие за ним числа, если они есть.
                    if (Char.IsDigit(expression[i]))
                        for (var j = i + 1; j < expression.Length && Char.IsDigit(expression[j]); j++)
                            s += expression[j];
                }
                // Возвращаем текущую часть строки.
                yield return s;
                // Передвигаем позицию в строке.
                i += s.Length;
            }
        }

        private static byte Priority(string s)      
        {
            // Получение приоритета операции.
            switch (s)
            {
                case "(":
                case ")":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                default:
                    return 3;
            }
        }
    }
}