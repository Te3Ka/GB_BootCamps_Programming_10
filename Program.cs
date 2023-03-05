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

int[] CountingSortExtended(int[] inputArray)
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
            sortedArray[index] = i - offset;
            index++;
        }
    }

    return sortedArray;
}


Random rand = new Random();
int[] array = new int[N].Select(r => rand.Next(0, 10)).ToArray();
Console.WriteLine("Изначальный массив:");
PrintArrayToConsole(array);
Console.WriteLine();
int[] arraySort = CountingSortExtended(array);
Console.WriteLine("Сортированный массив");
PrintArrayToConsole(arraySort);

/*
int[] resSerial = new int[N];
int[] resParallel = new int[N];

Array.Copy(array, resSerial, N);
Array.Copy(array, resParallel, N);
*/