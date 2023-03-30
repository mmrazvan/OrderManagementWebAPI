using OrderManagementWebAPI.DTOs;

namespace OrderManagementWebAPI.Helpers
{
    public static class DataHelpers
    {
        public static List<OrderLabels> CreateLabels(Orders order)
        {
            int totalCant = order.Quantity;
            int split = LabelManipulation.LabelsPerBox(order.PagesOnEnvelope, order.DocumentFormat);
            int dela = 1;
            int panala = dela + split - 1;
            int nrCutie = 1;

            List<OrderLabels> labels = new List<OrderLabels>();

            while (panala <= totalCant)
            {
                OrderLabels label = new OrderLabels
                {
                    OrderNumber = order.OrderNumber,
                    BoxNumber = nrCutie,
                    IdBoxNumber = GetIdBoxNumber(order.OrderNumber, nrCutie),
                    Quantity = split,
                    StartIndex = dela,
                    StopIndex = panala
                };
                nrCutie++;
                dela += split;
                panala += split;
                labels.Add(label);
            }
            if (totalCant - dela + 1 != 0)
            {
                OrderLabels label = new OrderLabels()
                {
                    OrderNumber = order.OrderNumber,
                    BoxNumber = nrCutie,
                    IdBoxNumber = GetIdBoxNumber(order.OrderNumber, nrCutie),
                    Quantity = totalCant - dela + 1,
                    StartIndex = dela,
                    StopIndex = totalCant
                };

                labels.Add(label);
            }
            return labels;
        }

        public static List<OrderTrace> CreateTraces(List<OrderLabels> labels)
        {
            var traces = new List<OrderTrace>();
            foreach (var label in labels)
            {
                OrderTrace trace = new OrderTrace
                {
                    IdBoxNumber = label.IdBoxNumber,
                    OrderNumber = label.OrderNumber
                };
                traces.Add(trace);
            }
            return traces;
        }
        public static string GetIdBoxNumber(int orderNumber, int boxNumber)
        {
            return orderNumber.ToString().PadLeft(6, '0') + boxNumber.ToString().PadLeft(4, '0');
        }
    }
}
