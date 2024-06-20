import { Component } from '@angular/core';
import { ArticuloService } from '../Service/Articulo/articulo.service';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { IArticulo } from '../Interfaces/IArticulo';
import { Router } from '@angular/router';
import { Observable, Subscriber } from 'rxjs';

@Component({
  selector: 'app-articulo-add',
  templateUrl: './articulo-add.component.html',
  styleUrls: ['./articulo-add.component.css']
})
export class ArticuloAddComponent {

articulo!:IArticulo;
imgBase64!:String;

sentArticulo:FormGroup=this.form.group({
  codigo: new FormControl(),
  descripcion: new FormControl(),
  precio: new FormControl()
});
constructor(private articuloService:ArticuloService,private form:FormBuilder,private router:Router) { }

onSubmit(data:any){
alert("dentro");
this.articulo.codigo=String(data.codigo);
this.articulo.descripcion=String(data.descripcion);
this.articulo.precio=Number(data.precio);
this.articulo.stock=0;
this.articulo.imagen=String(this.imgBase64);
console.log(this.articulo.imagen);
this.articuloService.post(this.articulo).subscribe(data=>{
  this.router.navigate([''])
});
}

onChange = ($event: Event)=>{

  const target = $event.target as HTMLInputElement;

  const file = (target.files as FileList)[0];

  this.convertImgToBase64(file);
}

convertImgToBase64(file:File){
  const observable = new Observable((subscriber: Subscriber<any>)=>{
    this.readFile(file,subscriber)
  });

  observable.subscribe((d)=>{
  this.imgBase64=String(d)
  console.log(this.imgBase64)
  })
}
readFile(file:File,subscriber:Subscriber<any>){
  const fileReader = new FileReader();
  fileReader.readAsDataURL(file);
  fileReader.onload=()=>{
    subscriber.next(fileReader.result);
    subscriber.complete();
  }

  fileReader.onerror = ()=>{
    subscriber.error()
    subscriber.complete()
  }
}

}
