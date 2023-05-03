using System;

class Program
{
    static void Main(string[] args)
    {
      Job job1 = new Job("Microsoft", "Software Engineer", 2019, 2022);
    Job job2 = new Job("Apple", "Manager", 2022, 2023);

    Resume myResume = new Resume("Kevin Lizzari");
    myResume.AddJob(job1);
    myResume.AddJob(job2);

    myResume.Display(); 
    }
}