import { useTasks } from "../hooks/useTasks";

export function TaskList() {
  const { listQuery, update, remove } = useTasks();

  if (listQuery.isLoading) return <p className="task-message">Loading tasks...</p>;
  if (listQuery.isError) return <p className="task-error">Error loading tasks.</p>;

  const tasks = listQuery.data ?? [];
  if (tasks.length === 0) return <p className="task-message">No tasks yet. Add one above.</p>;

  return (
    <ul className="task-list">
      {tasks.map((t) => (
        <li key={t.id} className="task-item">
          <label className="task-checkbox">
            <input
              type="checkbox"
              checked={t.isCompleted}
              onChange={() =>
                update.mutate({
                  id: t.id,
                  payload: {
                    title: t.title,
                    description: t.description ?? "",
                    isCompleted: !t.isCompleted,
                    dueDateUtc: t.dueDateUtc ?? null,
                    priority: t.priority,
                  },
                })
              }
            />
            Done
          </label>
          <div className="task-details">
            <span className="task-title">
              {t.title} ({["Low", "Medium", "High"][t.priority]})
            </span>
            {t.description && (
              <span className="task-description">{t.description}</span>
            )}
            {t.createdAtUtc && (
              <span className="task-due">
                Created: {new Date(t.createdAtUtc).toLocaleString()}
              </span>
            )}
            {t.dueDateUtc && (
              <span className="task-due">
                Due: {new Date(t.dueDateUtc).toLocaleString()}
              </span>
            )}
          </div>
          <button
            className="task-delete"
            onClick={() => remove.mutate(t.id)}
          >
            Delete
          </button>
        </li>
      ))}
    </ul>
  );
}
