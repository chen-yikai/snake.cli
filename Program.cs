using System;
using System.Threading;

public class MainClass
{
  static string[,] pixels = new string[30, 50];
  static int[,] snake = new int[500, 2];
  static int snakeLength = 1;
  static int snakeDirection = 2;

  static int[] food = new int[2];

  static int top = 0;
  static int right = 1;
  static int bottom = 2;
  static int left = 3;

  public static void Main(string[] args)
  {
    Console.ForegroundColor = ConsoleColor.Green;
    snake[0, 0] = 5;
    snake[0, 1] = 15;

    while (true)
    {
      InitPixels();
      BuildWall();
      // -----------------
      DetectKeyIn();
      RenderFood();
      RenderSnake();
      SnakeMotion();
      CheckFood();
      // -----------------
      RenderPixels();
      RenderInfo();
      HandleClearScreen();
    }
  }


  public static void CheckFood()
  {
    if (snake[0, 0] == food[0] && snake[0, 1] == food[1])
    {
      snakeLength++;
      food[0] = 0;
      food[1] = 0;
    }
  }

  public static void RenderFood()
  {
    if (food[0] == 0 || food[1] == 0)
    {
      Random random = new Random();
      int x = random.Next(1, 49);
      int y = random.Next(1, 29);
      food[0] = x;
      food[1] = y;
      RenderTarget(x, y, "F");
    }
    else
    {
      RenderTarget(food[0], food[1], "F");
    }
  }

  public static void DetectKeyIn()
  {
    if (Console.KeyAvailable)
    {
      ConsoleKeyInfo key = Console.ReadKey(true);
      switch (key.Key)
      {
        case ConsoleKey.UpArrow:
          if (snakeDirection != bottom)
            snakeDirection = top;
          break;
        case ConsoleKey.RightArrow:
          if (snakeDirection != left)
            snakeDirection = right;
          break;
        case ConsoleKey.DownArrow:
          if (snakeDirection != top)
            snakeDirection = bottom;
          break;
        case ConsoleKey.LeftArrow:
          if (snakeDirection != right)
            snakeDirection = left;
          break;
        case ConsoleKey.Q:
          Console.WriteLine("Exit Game");
          Environment.Exit(0);
          break;
        default:
          break;
      }
    }
  }

  public static void SnakeMotion()
  {
    int oldX = snake[0, 0];
    int oldY = snake[0, 1];
    switch (snakeDirection)
    {
      case 0: // Top
        snake[0, 1] -= 1;
        break;
      case 1: // Right
        snake[0, 0] += 1;
        break;
      case 2: // Bottom
        snake[0, 1] += 1;
        break;
      case 3: // Left
        snake[0, 0] -= 1;
        break;
    }
    // body
    for (int i = 1; i <= snakeLength; i++)
    {
      int tempX = snake[i, 0];
      int tempY = snake[i, 1];
      snake[i, 0] = oldX;
      snake[i, 1] = oldY;
      oldX = tempX;
      oldY = tempY;
    }
  }

  public static void RenderSnake()
  {
    for (int i = 0; i < snakeLength; i++)
    {
      if (i == 0)
        RenderTarget(snake[i, 0], snake[i, 1], "S");
      else
        RenderTarget(snake[i, 0], snake[i, 1], "o");
    }
  }

  public static void RenderTarget(int x, int y, string value)
  {
    if (x > 0 && x <= pixels.GetLength(1) - 2 && y > 0 && y <= pixels.GetLength(0) - 2)
    {
      // if (pixels[y, x] == "S" || pixels[y, x] == "o")
      // {
      //   Console.WriteLine("Game Over");
      //   Environment.Exit(0);
      // }
      if (pixels[y + 1, x + 1] == " " || pixels[y + 1, x + 1] == "F")
      {
        pixels[y + 1, x + 1] = value;
      }
    }
    else
    {
      Console.WriteLine("Game Over");
      Environment.Exit(0);
    }
  }

  public static void RevokeTarget(int x, int y)
  {
    pixels[y + 1, x + 1] = " ";
  }

  public static void InitPixels()
  {
    for (int i = 0; i < pixels.GetLength(0); i++)
    {
      for (int j = 0; j < pixels.GetLength(1); j++)
      {
        pixels[i, j] = " ";
      }
    }
  }

  public static void BuildWall()
  {
    // Top
    for (int i = 0; i < pixels.GetLength(1); i++)
    {
      pixels[0, i] = "#";
    }
    // Left
    for (int i = 0; i < pixels.GetLength(0); i++)
    {
      pixels[i, 0] = "#";
    }
    // Right
    for (int i = 0; i < pixels.GetLength(0); i++)
    {
      pixels[i, pixels.GetLength(1) - 1] = "#";
    }
    // Bottom
    for (int i = 0; i < pixels.GetLength(1); i++)
    {
      pixels[pixels.GetLength(0) - 1, i] = "#";
    }
  }

  public static void RenderPixels()
  {
    for (int i = 0; i < pixels.GetLength(0); i++)
    {
      Console.WriteLine();
      for (int j = 0; j < pixels.GetLength(1); j++)
      {
        Console.Write(pixels[i, j]);
      }
    }
  }

  public static void HandleClearScreen()
  {
    try
    {
      Thread.Sleep(250);
      Console.Clear();
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
    }
  }

  public static void RenderInfo()
  {
    Console.WriteLine("\n\nSnake Length: " + snakeLength);
    Console.WriteLine("\n\nSnake in CLI by EliasChen");
  }
}
