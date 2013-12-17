'use strict'

describe('Designer', function() {
    
    beforeEach(function() {
            module('DesignerApp');
        });
    
    describe('DesignerCtrl', function() {
    
        var scope = null,
            designerCtrl = null;
        
        beforeEach(inject(function($controller, $rootScope){
            scope = $rootScope.$new();
            designerCtrl = $controller('DesignerCtrl', { 
                $scope: scope
            });
        }));
        
        describe("#configurePager", function() {
         
            it('should have 1 page and 1 segment when there is 1 item (20 items per page)', function() {
                
                scope.$apply(function() {
                    scope.allItemsVirtualCount = 1;
                    scope.allItems.push({});
                });
                
                expect(scope.allItemsPages.length).toEqual(1);
                expect(scope.totalPageSegments).toEqual(1);
            });
            
            it('should have 2 pages and 1 segments when there is 23 items (20 items per page)', function() {
               
                scope.$apply(function() {
                   scope.allItemsVirtualCount = 23;
                   scope.allItems.push({});
                });
                
                expect(scope.allItemsPages.length).toEqual(2);
                expect(scope.totalPageSegments).toEqual(1);
            });
           
            it('should have 11 pages and 2 segments when there is 204 items (20 items per page)', function() {
               
                scope.$apply(function() {
                    scope.allItemsVirtualCount = 204;
                    scope.allItems.push({});
                });
                
                expect(scope.allItemsPages.length).toEqual(10);
                expect(scope.totalPageSegments).toEqual(2);
                
            });
            
        });
      
    });
    
});