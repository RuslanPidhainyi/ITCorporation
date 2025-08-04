import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Project, ProjectWithEmployees } from '../_models/project.model';
import { tap, switchMap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ProjectService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:7067/api/projects';

  // Signals
  projects = signal<Project[]>([]);
  selectedProject = signal<ProjectWithEmployees | null>(null);

  getProjects() {
    return this.http.get<Project[]>(this.baseUrl).subscribe({
      next: (projects) => this.projects.set(projects),
    });
  }

  getWithEmployees(id: number) {
    return this.http.get<ProjectWithEmployees>(`${this.baseUrl}/${id}/with-employees`).subscribe({
      next: (data) => this.selectedProject.set(data),
      error: (err) => console.error(`Failed to fetch project with employees (id: ${id})`, err),
    })
  }

  createProject(projectData: Partial<Project>) {
    return this.http.post<Project>(this.baseUrl, projectData).pipe(
      tap((newProject) => {
        this.projects.update((projects) => [...projects, newProject]);
      })
    ).subscribe();
  }

  deleteProject(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`).subscribe({
      next: () => this.projects.update(projects => projects.filter(p => p.Id !== id)),
      error: (err) => console.error(`Failed to delete project (id: ${id})`, err)
    });
  }

  updateProject(id: number, projectData: Partial<Project>) {
    return this.http.put(`${this.baseUrl}/${id}`, projectData).pipe(
      switchMap(() => this.http.get<Project>(`${this.baseUrl}/${id}`)),
      tap((updatedProject) => {
        this.projects.update((projects) =>
          projects.map((p) => (p.Id === id ? updatedProject : p))
        );
      })
    ).subscribe();
  }
}