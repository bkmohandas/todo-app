import { QueryClientProvider } from "@tanstack/react-query";
import { queryClient } from "./lib/queryClient";
import { TaskForm } from "./components/TaskForm";
import { TaskList } from "./components/TaskList";
import "./App.css";

export default function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <main className="app-main">
        <h1 className="app-title">To Do</h1>
        <TaskForm />
        <h2 className="task-list-heading">Current tasks</h2>
        <TaskList />
      </main>
    </QueryClientProvider>
  );
}