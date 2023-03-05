const int THREAD_NUMBERS = 2; //число потоков
const int N = 10; //размер массива

//Печать массива в консоль
void PrintArrayToConsole(int[] printArray)
{
    Console.Write("[");
    for (int i = 0; i < printArray.Length; i++)
    {
        if (i == printArray.Length - 1)
            Console.Write($"{printArray[i]}");
        else
            Console.Write($"{printArray[i]}, ");
    }
    Console.Write("]");
}

//Сортировка одним потоком
void CountingSortExtended(int[] inputArray)
{
    int max = inputArray.Max();
    int min = inputArray.Min();

    int offset = -min;
    int[] sortedArray = new int[inputArray.Length];
    int[] counters = new int[max + offset + 1];

    for (int i = 0; i < inputArray.Length; i++)
    {
        counters[inputArray[i] + offset]++;
    }

    int index = 0;
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++)
        {
            inputArray[index] = i - offset;
            index++;
        }
    }
}

//Подготовка к параллельной сортировке
void PrepareParallelSorting(int[] inputArray)
{
    int max = inputArray.Max();
    int min = inputArray.Min();

    int offset = -min;
    int[] counters = new int[max + offset + 1];

    int eachThreadCalc = N / THREAD_NUMBERS;
    var threadsList = new List<Thread>();

    for (int i = 0; i < THREAD_NUMBERS; i++)
    {
        int startPos = i * eachThreadCalc;
        int endPos = (i + 1) * eachThreadCalc;
        if (i == THREAD_NUMBERS - 1) endPos = N;
        
        threadsList.Add(new Thread(() => ParallelSortExtended(inputArray, counters, startPos, endPos, offset)));
    	threadsList[i].Start();
    }

    foreach(var thread in threadsList)
    {
    	thread.Join();
    }

    int index = 0;
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++)
        {
            inputArray[index] = i - offset;
            index++;
        }
    }
}


//Сортировка несколькими потоками
void ParallelSortExtended(int[] inputArray, int[] counters, int startPos, int endPos, int offset)
{
    for (int i = startPos; i < endPos; i++)
    {
        counters[inputArray[i] + offset]++;
    }
}

Random rand = new Random();
int[] array = new int[N].Select(r => rand.Next(0, 10)).ToArray();
Console.WriteLine("Изначальный массив готов.");

int[] resSerial = new int[N];
int[] resParallel = new int[N];

Array.Copy(array, resSerial, N);
CountingSortExtended(resSerial);
Console.WriteLine("Сортировка одним потоком готова");

Array.Copy(array, resParallel, N);
PrepareParallelSorting(resParallel);
Console.WriteLine("Сортировка несколькими потоками готова.");
