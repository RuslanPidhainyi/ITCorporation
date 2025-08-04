import { Employee } from './employee.model';

export interface Project {
  Id: number;
  Name: string;
  Status: string;
}

export interface ProjectWithEmployees extends Project {
  Employees: Employee[];
}