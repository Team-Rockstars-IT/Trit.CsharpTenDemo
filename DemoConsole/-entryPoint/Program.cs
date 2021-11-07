using DemoConsole = Trit.DemoConsole;

while (true)
{
    WriteLine();
    WriteLine("Pick a hex number: ");
    var picked = ReadKey().KeyChar;
    Clear();

    await (picked switch
    {
        '1' => DemoConsole._1_Namespaces_and_usings.Demo.Main(),
        '2' => DemoConsole._2_Interpolated_strings.Demo.Main(),
        '3' => DemoConsole._3_Lambdas.Demo.Main(),
        '4' => DemoConsole._4_Records.Demo.Main(),
        '5' => DemoConsole._5_Structs.Demo.Main(),
        '6' => DemoConsole._6_CallerArgumentExpression.Demo.Main(),
        '7' => DemoConsole._7_Pattern_matching.Demo.Main(),
        '8' => DemoConsole._8_Generic_attributes.Demo.Main(),
        '9' => DemoConsole._9_MethodBuilder.Demo.Main(),
        'A' => DemoConsole.A_Linq.Demo.Main(),
        'B' => DemoConsole.B_Parallel.Demo.Main(),
        'C' => DemoConsole.C_DateTime.Demo.Main(),
        'D' => DemoConsole.D_Reflection.Demo.Main(),
        'E' => DemoConsole.E_Timer.Demo.Main(),
        'F' => DemoConsole.F_PriorityQueue.Demo.Main(),
        _ => Task.CompletedTask
    });

    if (picked == 'Q')
    {
        return 0;
    }

    Console.ReadKey();
}