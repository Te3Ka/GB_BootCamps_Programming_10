const int THREAD_NUMBERS = 2; //число потоков
const int N = 500_000; //размер массива

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


Random rand = new Random();
int[] array = new int[N].Select(r => rand.Next(0, 10)).ToArray();
Console.WriteLine("Изначальный массив:");
PrintArrayToConsole(array);
Console.WriteLine();

int[] resSerial = new int[N];
int[] resParallel = new int[N];