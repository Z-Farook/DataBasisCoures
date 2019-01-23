using System;
using System.Collections.Generic;
using System.Linq;


namespace MapReduce
{
    public static class MapReduceExtension
    {
        public static IEnumerable<MapReturn> Map<MapIntput, MapReturn>(this IEnumerable<MapIntput> inputTable, Func<MapIntput, MapReturn> mapper)
        {

            MapReturn[] result = new MapReturn[inputTable.Count()];

            for (int i = 0; i < inputTable.Count(); i++)
            //     Returns the elements of the specified sequence or the specified value in a singleton
            //     collection if the sequence is empty.
            {
                result[i] = mapper(inputTable.ElementAt(i));
                //     Returns the element at a specified index in a sequence or a default value if
                //     the index is out of range.
            }
            return result;
        }
        public static State Reduce<reduceInput, State>(this IEnumerable<reduceInput> table, State init, Func<State, reduceInput, State> operation)
        {
            State accumulator = init;
            for (int i = 0; i < table.Count(); i++)
            {
                accumulator = operation(accumulator, table.ElementAt(i));
            }
            return accumulator;
        }
        public static IEnumerable<Tuple<tb1, tb2>> joinFunc<tb1, tb2>(this IEnumerable<tb1> leftTbale, IEnumerable<tb2> rightTbale, Func<Tuple<tb1, tb2>, bool> filter)
        {
            return
              leftTbale.Reduce(new List<Tuple<tb1, tb2>>(),
               (leftTb, leftRow) =>
              {
                  List<Tuple<tb1, tb2>> combinedTb =
                rightTbale.Reduce(new List<Tuple<tb1, tb2>>(),
                (rightTb, rightRow) =>
                {
                    Tuple<tb1, tb2> combiTbTuple = new Tuple<tb1, tb2>(leftRow, rightRow);
                    if (filter(combiTbTuple))
                        rightTb.Add(combiTbTuple);
                    return rightTb;
                });
                  leftTb.AddRange(combinedTb);
                  return leftTb;
              });
        }
    }
}