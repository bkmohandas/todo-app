import { AxiosError } from "axios";
import { api } from "./api";

export type TaskDto = {
  id: number;
  title: string;
  description?: string | null;
  isCompleted: boolean;
  createdAtUtc: string;
  dueDateUtc?: string | null;
  priority: number;
};

export type TaskCreateDto = {
  title: string;
  description?: string | null;
  dueDateUtc?: string | null;
  priority: number;
};

export type TaskUpdateDto = {
  title: string;
  description?: string | null;
  isCompleted: boolean;
  dueDateUtc?: string | null;
  priority: number;
};

// Fetch all tasks
export const getTasks = async (): Promise<TaskDto[]> => {
  try {
    const response = await api.get<TaskDto[]>("/tasks");
    return response.data;
  } catch (error) {
    handleApiError(error, "Failed to fetch tasks");
  }
};

// Fetch a single task by ID
export const getTask = async (id: number): Promise<TaskDto> => {
  try {
    const response = await api.get<TaskDto>(`/tasks/${id}`);
    return response.data;
  } catch (error) {
    handleApiError(error, `Failed to fetch task with id ${id}`);
  }
};

// Create a new task
export const createTask = async (payload: TaskCreateDto): Promise<TaskDto> => {
  try {
    const response = await api.post<TaskDto>("/tasks", payload);
    return response.data;
  } catch (error) {
    handleApiError(error, "Failed to create task");
  }
};

// Update an existing task
export const updateTask = async (
  id: number,
  payload: TaskUpdateDto
): Promise<TaskDto> => {
  try {
    const response = await api.put<TaskDto>(`/tasks/${id}`, payload);
    return response.data;
  } catch (error) {
    handleApiError(error, `Failed to update task with id ${id}`);
  }
};

// Delete a task
export const deleteTask = async (id: number): Promise<void> => {
  try {
    await api.delete(`/tasks/${id}`);
  } catch (error) {
    handleApiError(error, `Failed to delete task with id ${id}`);
  }
};

// Centralized error handler
function handleApiError(error: unknown, message: string): never {
  if (error instanceof AxiosError) {
    throw new Error(`${message}: ${error.response?.status} ${error.response?.statusText}`);
  }
  throw new Error(message);
}