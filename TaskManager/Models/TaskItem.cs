using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace TaskManager1.Models
{
    public class TaskItem : INotifyPropertyChanged
    {
        private string _title = string.Empty;
        private string _description = string.Empty;
        private string _tags = string.Empty;
        private int _priorityIndex = 1;
        private bool _isDone;

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); OnPropertyChanged(nameof(DisplayTitle)); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public string Tags
        {
            get => _tags;
            set { _tags = value; OnPropertyChanged(); }
        }

        public int PriorityIndex
        {
            get => _priorityIndex;
            set { _priorityIndex = value; OnPropertyChanged(); OnPropertyChanged(nameof(PriorityLabel)); }
        }

        public bool IsDone
        {
            get => _isDone;
            set { _isDone = value; OnPropertyChanged(); OnPropertyChanged(nameof(DisplayTitle)); }
        }

        public ObservableCollection<TaskItem> Children { get; set; } = new ObservableCollection<TaskItem>();

        [JsonIgnore]
        public string PriorityLabel => PriorityIndex switch
        {
            0 => "Низкий",
            1 => "Обычный",
            2 => "Высокий",
            _ => "—"
        };

        [JsonIgnore]
        public string DisplayTitle => IsDone ? $"✔ {Title}" : Title;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
