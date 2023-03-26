import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, of, tap } from 'rxjs';
interface itemPush{
    item1:string;
    item2:string;
}
@Injectable({
  providedIn: 'root',
})
export class ContractData {
  
  baseUrl: string = '';
  constructor(private http: HttpClient) {
    this.baseUrl = environment.url;
  }
  public getDocuments(): Observable<string[]> {
    var url = this.baseUrl + 'Documents/contractNames';
    return this.http
      .get<string[]>(url)
      .pipe(tap((it) => console.log('received', it)));
  }
  getDocumentReplace(name: string) :Observable<string[]>{
    var url = this.baseUrl + 'DocReplacer/ReplacerNames/'+name;
    return this.http
      .get<string[]>(url)
      .pipe(tap((it) => console.log('received', it)));
  }

  saveReplacements(name: string , data: Map<string,string>) :Observable<any>{
    var url = this.baseUrl + 'DocReplacer/ReplaceData/?filename='+name;
    var obj:Array<itemPush>=[];

    data.forEach((value, key) => {
        obj.push({item1: key,item2:value});
    })

    return this.http
      .post(url,
        obj, {
        responseType: 'blob',
      });
      
  }
}
