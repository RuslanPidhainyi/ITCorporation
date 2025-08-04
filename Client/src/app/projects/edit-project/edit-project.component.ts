import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProjectService } from '../../_services/project.service';
import { Project } from '../../_models/project.model';

@Component({
  selector: 'app-edit-project',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './edit-project.component.html',
  styleUrls: ['./edit-project.component.css'],
})
export class EditProjectComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private projectService = inject(ProjectService);

  name: string = '';
  status: string = 'ToDo';
  id!: number;

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    const project = this.projectService
      .projects()
      .find((p) => p.Id === this.id);
    if (project) {
      this.name = project.Name;
      this.status = project.Status;
    }
  }

  updateProject() {
    const updated = {
      Name: this.name,
      Status: this.status,
    };
    this.projectService.updateProject(this.id, updated);
    this.router.navigate(['/projects/dashboard']);
  }
}