import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DocumentSingleComponent } from './document-single/document-single.component';

const routes: Routes = [
  { path: '', redirectTo: '/documents', pathMatch: 'full'},
  {  path: 'documents', component: DocumentSingleComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
