import { Component, inject, OnInit } from '@angular/core';
import { CommonModule, NgClass, NgFor, NgIf } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { ProjectService } from '../../_services/project.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-project-list',
  standalone: true,
  imports: [NgFor, NgIf, NgClass, CommonModule, RouterModule, HttpClientModule],
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {
    projectService = inject(ProjectService);
      router = inject(Router);
    
    ngOnInit(): void {
        this.loadProjects();
    }
    
    loadProjects() {
        this.projectService.getProjects();
    }
    
    navigateToAdd() {
        this.router.navigate(['/projects/add']);
    }
    
    navigateToDetails(projectId: number) {
      this.router.navigate(['/projects', projectId]);
    }

    editProject(projectId: number) {
      this.router.navigate(['/projects/edit', projectId]);
    }

    deleteProject(projectId: number) {
      if (confirm('Are you sure you want to delete this project?')) {
        this.projectService.deleteProject(projectId);
      }
    }
}