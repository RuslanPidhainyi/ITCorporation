import { Routes } from '@angular/router';
import { AddProjectComponent } from './projects/add-project/add-project.component';
import { ProjectListComponent } from './projects/project-list/project-list.component';
import { ProjectDetailsComponent } from './projects/project-details/project-details.component';
import { EditProjectComponent } from './projects/edit-project/edit-project.component';

export const routes: Routes = [
    { path: '', component: ProjectListComponent },
    { path: 'projects/dashboard', component: ProjectListComponent },
    { path: 'projects/add', component: AddProjectComponent  },
    { path: 'projects/edit/:id', component: EditProjectComponent },
    { path: 'projects/:id', component: ProjectDetailsComponent },
];