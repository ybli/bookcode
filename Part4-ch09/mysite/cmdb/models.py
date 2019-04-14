from django.db import models

# Create your models here.
class employee(models.Model):
    id=models.IntegerField( blank=True, null=False,primary_key=True)
    username = models.CharField(max_length=255, blank=True, null=True)
    password = models.CharField(max_length=255, blank=True, null=True)


    class Meta:
        managed = True
        db_table = 'cmdb_employee'
