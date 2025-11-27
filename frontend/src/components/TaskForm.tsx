import { useState } from "react";
import { useTasks } from "../hooks/useTasks";

export function TaskForm() {
  const { create } = useTasks();
  const [title, setTitle] = useState("");
  const [priority, setPriority] = useState(0);
  const [description, setDescription] = useState("");
  const [dueDateUtc, setDueDateUtc] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);

  const submit = (e: React.FormEvent) => {
    e.preventDefault();

    if (!title.trim()) {
      setError("Title is required.");
      return;
    }

    let utcDate: string | null = null;
    if (dueDateUtc) {
      // Convert local datetime string to UTC ISO string
      utcDate = new Date(dueDateUtc).toISOString();
    }

    create.mutate({
      title,
      priority,
      description: description || null,
      dueDateUtc: utcDate,
    });

    setTitle("");
    setPriority(0);
    setDescription("");
    setDueDateUtc(null);
    setError(null);
  };

  return (
    <form onSubmit={submit} className="task-form">
      <label className="task-label">
        Task title
        <input
          className="task-input"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Enter a short title"
          maxLength={100}
        />
      </label>
      {error && <span className="task-error">{error}</span>}
      <label className="task-label">
        Task description
        <textarea
          className="task-textarea"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder="Optional details"
          maxLength={500}
        />
      </label>
      <label className="task-label">
        Due date & time
        <input
          className="task-date"
          type="datetime-local"
          value={dueDateUtc ?? ""}
          onChange={(e) => setDueDateUtc(e.target.value || null)}
        />
      </label>
      <label className="task-label">
        Priority
        <select
          className="task-select"
          value={priority}
          onChange={(e) => setPriority(Number(e.target.value))}
        >
          <option value={0}>Low</option>
          <option value={1}>Medium</option>
          <option value={2}>High</option>
        </select>
      </label>
      <button className="task-button" type="submit" disabled={create.isPending}>
        Add
      </button>
      {create.isError && (
        <span className="task-error">Failed to add task.</span>
      )}
    </form>
  );
}
