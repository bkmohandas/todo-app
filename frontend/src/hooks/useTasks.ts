import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { createTask, deleteTask, getTasks, updateTask, type TaskCreateDto, type TaskUpdateDto } from "../services/tasks";

export const useTasks = () => {
  const qc = useQueryClient();
  const listQuery = useQuery({ queryKey: ["tasks"], queryFn: getTasks });

  const create = useMutation({
    mutationFn: (payload: TaskCreateDto) => createTask(payload),
    onSuccess: () => qc.invalidateQueries({ queryKey: ["tasks"] }),
  });

  const update = useMutation({
    mutationFn: ({ id, payload }: { id: number; payload: TaskUpdateDto }) => updateTask(id, payload),
    onSuccess: () => qc.invalidateQueries({ queryKey: ["tasks"] }),
  });

  const remove = useMutation({
    mutationFn: (id: number) => deleteTask(id),
    onSuccess: () => qc.invalidateQueries({ queryKey: ["tasks"] }),
  });

  return { listQuery, create, update, remove };
};
