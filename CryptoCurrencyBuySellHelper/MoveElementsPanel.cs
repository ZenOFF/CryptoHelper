using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NoviceCryptoTraderAdvisor
{
    /// <summary>
    /// Выполняет перемещение элементов на основе сортированного списка
    /// </summary>
    internal class MoveElementsPanel
    {
        private Panel _mainPanel;
        private List<MarketPair> _referenceListPairs;
        private List<MarketPair> _onShowListPairs;

        /// <summary>
        /// Инициализация списков
        /// </summary>
        /// <param name="mainPanel">Панель содержащая элементы </param>
        /// <param name="referenceListPairs">Эталонный(сортированый список) элементов</param>
        /// <param name="onShowListPairs">Видимый(отображённый список) элементов</param>
        public MoveElementsPanel(Panel mainPanel, List<MarketPair> referenceListPairs, List<MarketPair> onShowListPairs)
        {
            this._mainPanel = mainPanel;
            this._referenceListPairs = referenceListPairs;
            this._onShowListPairs = onShowListPairs;
        }

        public List<MarketPair> ReLocationElement()
        {
            int currentIndex = 0;
            _mainPanel.AutoScroll = false;
            foreach (var refPair in _referenceListPairs)
            {
                string _name = refPair._marketName;
                int _posY = refPair.PosY;
                //поиск пары
                MarketPair pair = _onShowListPairs.Find(x => x._marketName == _name);
                //получаем индекс пары в не сортированном списке
                int indexShowPair = _onShowListPairs.IndexOf(pair);
                // если индексы не совпадают то перемещаем  в позицию как в сортированном списке
                if (currentIndex != indexShowPair)
                {
                    pair.PanelMoveTo(NewLocationForPanel(currentIndex));
                }
                currentIndex++;
            }

            _mainPanel.AutoScroll = true;
            return _onShowListPairs;
        }

        //Вычисляем позиция из колличества отрисованых панелей
        private Point NewLocationForPanel(int Index)
        {
            Point Loc = new Point(12, 12);
            if (Index >= 1)
            {
                Loc.X = 12;
                Loc.Y = 12 + Index * 160;
            }
            return Loc;
        }
    }
}