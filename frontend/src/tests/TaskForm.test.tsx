import { describe, it, expect } from "vitest";
import { render, screen } from "@testing-library/react";
import { QueryClientProvider } from "@tanstack/react-query";
import { queryClient } from "../lib/queryClient";
import { TaskForm } from "../components/TaskForm";

describe("TaskForm", () => {
  it("renders input and button", () => {
    render(
      <QueryClientProvider client={queryClient}>
        <TaskForm />
      </QueryClientProvider>
    );
    expect(screen.getByPlaceholderText("Enter a short title")).toBeInTheDocument();
    expect(screen.getByText("Add")).toBeInTheDocument();
  });
});