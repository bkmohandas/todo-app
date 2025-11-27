# ToDoApp Frontend

A frontend built with **React**, **TypeScript**, and **Vite**.  
This application provides a clean, accessible interface for managing tasks.

---

## Tech Stack

- **Framework:** React 18  
- **Language:** TypeScript  
- **Bundler/Dev Server:** Vite  
- **Styling:** CSS modules
- **Testing:** React Testing Library + Vitest  

---

## Setup Steps

### Prerequisites
- Node.js (recommended v22.15.1)
- npm

### Install dependencies
```bash
cd frontend
npm install
```

### Start development server
```bash
npm run dev
```

### Validation Rules (Frontend)
- Title: required, max 100 characters
- Description: optional, max 500 characters
- Priority: must be Low, Medium, or High
- Due date: optional, must be in the future

### Testing
```bash
cd frontend
npm run test
```

### Future Improvements
- Add authentication and user accounts
- Implement filtering, sorting, and pagination for tasks
- Improve both unit test coverage and end‑to‑end tests
- Integrate error monitoring for observability