using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using TaskManager1.Models;
using TaskManager1.Services;

namespace TaskManager1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        // ===== Collection principale =====
        public ObservableCollection<TaskItem> Tasks { get; } = new ObservableCollection<TaskItem>();

        // ===== Vue filtrée =====
        public ICollectionView TasksView { get; }

        // ===== Saisie =====
        private string _titleInput = string.Empty;
        public string TitleInput
        {
            get => _titleInput;
            set { _titleInput = value; OnPropertyChanged(); OnPropertyChanged(nameof(Error)); }
        }

        private string _tagsInput = string.Empty;
        public string TagsInput
        {
            get => _tagsInput;
            set { _tagsInput = value; OnPropertyChanged(); }
        }

        private string _descriptionInput = string.Empty;
        public string DescriptionInput
        {
            get => _descriptionInput;
            set { _descriptionInput = value; OnPropertyChanged(); }
        }

        private int _priorityIndex = 1;
        public int PriorityIndex
        {
            get => _priorityIndex;
            set { _priorityIndex = value; OnPropertyChanged(); }
        }

        private TaskItem? _selectedTask;
        public TaskItem? SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
                if (value != null)
                {
                    TitleInput = value.Title;
                    DescriptionInput = value.Description;
                    TagsInput = value.Tags;
                    PriorityIndex = value.PriorityIndex;
                }
            }
        }

        // ===== Filtres =====
        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); TasksView.Refresh(); }
        }

        private int _filterPriorityIndex = 0; // 0 = "Все", 1 = Низкий, 2 = Обычный, 3 = Высокий
        public int FilterPriorityIndex
        {
            get => _filterPriorityIndex;
            set { _filterPriorityIndex = value; OnPropertyChanged(); TasksView.Refresh(); }
        }

        // ===== Statut =====
        private string _statusText = "Prêt";
        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(); }
        }

        public int TaskCount => Tasks.Count;
        public int DoneCount => Tasks.Count(t => t.IsDone);
        public int PendingCount => Tasks.Count(t => !t.IsDone);

        // ===== Commandes =====
        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand SaveTaskCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand AboutCommand { get; }
        public ICommand ToggleDoneCommand { get; }
        public ICommand AddSubTaskCommand { get; }

        // ===== Validation =====
        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(TitleInput))
                {
                    if (string.IsNullOrWhiteSpace(TitleInput))
                        return "Название не может быть пустым";
                    if (TitleInput.Length < 3)
                        return "Минимум 3 символа";
                }
                return string.Empty;
            }
        }

        public MainViewModel()
        {
            // Chargement JSON au démarrage
            foreach (var t in JsonTaskService.Load()) Tasks.Add(t);

            // Vue filtrée
            TasksView = CollectionViewSource.GetDefaultView(Tasks);
            TasksView.Filter = FilterTask;

            // Commandes
            AddTaskCommand    = new RelayCommand(_ => AddTask(), _ => IsValid());
            DeleteTaskCommand = new RelayCommand(_ => DeleteTask(), _ => SelectedTask != null);
            SaveTaskCommand   = new RelayCommand(_ => SaveAll());
            CancelCommand     = new RelayCommand(_ => Cancel());
            ExitCommand       = new RelayCommand(_ => Application.Current.Shutdown());
            AboutCommand      = new RelayCommand(_ => MessageBox.Show(
                "TaskManager — ЛР №6\nMVVM + Validation + Filtres + JSON",
                "О программе"));
            ToggleDoneCommand = new RelayCommand(p => ToggleDone(p as TaskItem));
            AddSubTaskCommand = new RelayCommand(p => AddSubTask(p as TaskItem));

            Tasks.CollectionChanged += (_, __) => RefreshStats();
        }

        private bool FilterTask(object obj)
        {
            if (obj is not TaskItem t) return false;

            if (!string.IsNullOrWhiteSpace(SearchText) &&
                !(t.Title ?? string.Empty).Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                return false;

            // index 0 = "Все" ; sinon index - 1 = priorité réelle (0,1,2)
            if (FilterPriorityIndex > 0 && t.PriorityIndex != (FilterPriorityIndex - 1))
                return false;

            return true;
        }

        private bool IsValid() => string.IsNullOrEmpty(this[nameof(TitleInput)]);

        private void AddTask()
        {
            var task = new TaskItem
            {
                Title = TitleInput,
                Description = DescriptionInput,
                Tags = TagsInput,
                PriorityIndex = PriorityIndex
            };
            Tasks.Add(task);
            StatusText = $"Ajouté : {task.Title}";
            ResetFields();
        }

        private void DeleteTask()
        {
            if (SelectedTask == null) return;
            Tasks.Remove(SelectedTask);
            StatusText = $"Supprimé : {SelectedTask.Title}";
            SelectedTask = null;
            ResetFields();
        }

        private void ToggleDone(TaskItem? task)
        {
            if (task == null) return;
            task.IsDone = !task.IsDone;
            StatusText = $"{(task.IsDone ? "Terminée" : "En cours")} : {task.Title}";
            RefreshStats();
        }

        private void AddSubTask(TaskItem? parent)
        {
            if (parent == null) return;
            parent.Children.Add(new TaskItem
            {
                Title = $"Sous-tâche de {parent.Title}",
                PriorityIndex = parent.PriorityIndex
            });
        }

        private void SaveAll()
        {
            JsonTaskService.Save(Tasks);
            StatusText = "Sauvegardé dans tasks.json";
        }

        private void Cancel()
        {
            ResetFields();
            SelectedTask = null;
            StatusText = "Annulé";
        }

        private void ResetFields()
        {
            TitleInput = string.Empty;
            DescriptionInput = string.Empty;
            TagsInput = string.Empty;
            PriorityIndex = 1;
        }

        private void RefreshStats()
        {
            OnPropertyChanged(nameof(TaskCount));
            OnPropertyChanged(nameof(DoneCount));
            OnPropertyChanged(nameof(PendingCount));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}