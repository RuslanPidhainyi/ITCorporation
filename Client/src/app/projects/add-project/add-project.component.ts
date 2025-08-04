import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProjectService } from '../../_services/project.service';

@Component({
  selector: 'app-add-project',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-project.component.html',
  styleUrl: './add-project.component.css'
})
export class AddProjectComponent {
  projectService = inject(ProjectService);
  router = inject(Router);

  nameValue: string = '';
  statusValue: string = 'ToDo';

  addProject() {
    const newProject = {
      Name: this.nameValue,
      Status: this.statusValue
    };

    this.projectService.createProject(newProject);
    this.router.navigate(['projects/dashboard']);
  }
}