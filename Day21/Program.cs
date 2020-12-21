using System;
using System.Collections.Generic;
using System.Linq;

namespace Day21
{
    public class Program
    {
        static void Main(string[] args)
        {
            HashSet<HashSet<string>> allRecipies = new HashSet<HashSet<string>>();
            Dictionary<string, HashSet<string>> allergenToIngredients = new Dictionary<string, HashSet<string>>();
            Dictionary<string, string> ingredientToAllergen = new Dictionary<string, string>();

            Utils.Utils.ReadLinesFromConsole().ToList().ForEach(line =>
            {
                string[] splitted = line.Split("(contains");
                HashSet<string> ingredients = splitted[0].Trim().Split().ToHashSet();
                HashSet<string> allergens = splitted[1].Trim().Trim(')').Split(", ").ToHashSet();
                allRecipies.Add(ingredients);

                foreach (string allergen in allergens)
                {
                    if (!allergenToIngredients.ContainsKey(allergen))
                    {
                        allergenToIngredients[allergen] = new HashSet<string>(ingredients);
                    }
                    else
                    {
                        allergenToIngredients[allergen] = allergenToIngredients[allergen].Intersect(ingredients).ToHashSet();
                    }
                }
            });

            Print(allergenToIngredients);

            bool hasChanged = true;
            while (hasChanged)
            {
                hasChanged = false;
                string foundIngredient = null;
                foreach (var kvp in allergenToIngredients)
                {
                    if (kvp.Value.Count == 1)
                    {
                        foundIngredient = kvp.Value.First();
                        ingredientToAllergen[foundIngredient] = kvp.Key;
                        hasChanged = true;
                        break;
                    }
                }

                // Clear found ingredient from all allergens
                foreach (var kvp in allergenToIngredients)
                {
                    if (kvp.Value.Contains(foundIngredient))
                    {
                        kvp.Value.Remove(foundIngredient);
                    }
                }
            }
            Console.WriteLine("--");
            Print(ingredientToAllergen);
            Console.WriteLine("--");
            Console.WriteLine($"Part 1 : {allRecipies.Sum(recipie => recipie.Count(ingredient => !ingredientToAllergen.ContainsKey(ingredient)))}");
            Console.WriteLine("--");
            Console.WriteLine(string.Join(',', ingredientToAllergen.OrderBy(_ => _.Value).Select(_ => _.Key)));
        }

        static void Print(Dictionary<string, HashSet<string>> allergenToIngredients)
        {
            foreach (var kvp in allergenToIngredients)
            {
                Console.WriteLine($"{kvp.Key} is in one of {string.Join(" ", kvp.Value)}");
            }
        }

        static void Print(Dictionary<string, string> ingredientToAllergen)
        {
            foreach (var kvp in ingredientToAllergen)
            {
                Console.WriteLine($"{kvp.Key} has {kvp.Value}");
            }
        }
    }
}
